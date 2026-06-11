import { createContext, useContext, useState, useEffect, type ReactNode } from 'react'
import type { LoginResponse } from '../types'
import { login as apiLogin } from '../api/auth'

interface AuthContextType {
  user: LoginResponse | null
  userId: string | null  // GUID from JWT sub claim
  isAdmin: boolean
  login: (email: string, password: string) => Promise<void>
  logout: () => void
  isLoading: boolean
}

function decodeUserId(token: string): string | null {
  try {
    const payload = JSON.parse(atob(token.split('.')[1]))
    return payload.sub ?? null
  } catch {
    return null
  }
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<LoginResponse | null>(null)
  const [userId, setUserId] = useState<string | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const stored = localStorage.getItem('user')
    const token = localStorage.getItem('token')
    if (stored) setUser(JSON.parse(stored))
    if (token) setUserId(decodeUserId(token))
    setIsLoading(false)
  }, [])

  const login = async (email: string, password: string) => {
    const data = await apiLogin(email, password)
    localStorage.setItem('token', data.accessToken)
    localStorage.setItem('user', JSON.stringify(data))
    setUser(data)
    setUserId(decodeUserId(data.accessToken))
  }

  const logout = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    setUser(null)
    setUserId(null)
  }

  const isAdmin = user?.role === 'Admin'

  return (
    <AuthContext.Provider value={{ user, userId, isAdmin, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}
