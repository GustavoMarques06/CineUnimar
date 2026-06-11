import client from './client'
import type { LoginResponse } from '../types'

export const login = (email: string, password: string) =>
  client.post<LoginResponse>('/auth/login', { email, password }).then((r) => r.data)

export const register = (data: {
  firstName: string
  lastName: string
  email: string
  password: string
  cpf: string
  dateOfBirth: string
  role: number
}) => client.post('/auth/register', data).then((r) => r.data)
