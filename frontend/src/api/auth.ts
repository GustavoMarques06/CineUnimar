import client from './client'

export const login = (email: string, password: string) =>
  client.post<{ userName: string; role: string; expiresAt: string }>('/auth/login', { email, password }).then((r) => r.data)

export const logout = () =>
  client.post('/auth/logout').then((r) => r.data)

export const me = () =>
  client.get<{ userId: string; email: string; userName: string; role: string }>('/auth/me').then((r) => r.data)

export const register = (data: {
  firstName: string
  lastName: string
  email: string
  password: string
  cpf: string
  dateOfBirth: string
  role: number
}) => client.post('/auth/register', data).then((r) => r.data)
