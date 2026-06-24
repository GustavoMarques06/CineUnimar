import { createContext, useContext, useState, useEffect, type ReactNode } from 'react'
import { login as apiLogin, logout as apiLogout, me as apiMe } from '../api/auth'

interface UserInfo {
  userId: string
  email: string
  userName: string
  role: string
}

interface AuthContextType {
  user: UserInfo | null
  isAdmin: boolean
  login: (email: string, password: string) => Promise<void>
  logout: () => Promise<void>
  isLoading: boolean
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<UserInfo | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  // Verifica sessão via cookie httpOnly ao carregar o app
  useEffect(() => {
    apiMe()
      .then((data) => setUser(data))
      .catch(() => setUser(null))
      .finally(() => setIsLoading(false))
  }, [])

  const login = async (email: string, password: string) => {
    // O backend seta o cookie httpOnly; recebemos apenas os dados de exibição
    const data = await apiLogin(email, password)
    // Busca as informações completas (incluindo userId) via /me
    const info = await apiMe()
    setUser(info)
    // Suprime aviso — data.userName/role usados apenas para validação de sucesso
    void data
  }

  const logout = async () => {
    await apiLogout()
    setUser(null)
  }

  // isAdmin vem de dados verificados pelo servidor — não do localStorage
  const isAdmin = user?.role === 'Admin'

  return (
    <AuthContext.Provider value={{ user, isAdmin, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}
