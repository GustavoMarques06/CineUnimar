import axios from 'axios'

// withCredentials envia o cookie httpOnly em todas as requisições automaticamente
const client = axios.create({ baseURL: '/api', withCredentials: true })

// Redireciona para login em respostas 401 (sessão expirada ou não autenticado).
client.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error?.response?.status === 401 && window.location.pathname !== '/login') {
      window.location.href = '/login'
    }
    return Promise.reject(error)
  }
)

// Helper to extract value from DDD value objects (string or { value: string })
// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const val = (v: any): string => {
  if (v == null) return ''
  if (typeof v === 'string') return v
  if (typeof v === 'number') return String(v)
  if (typeof v === 'object' && 'value' in v) return String(v.value)
  return String(v)
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const numVal = (v: any): number => {
  if (v == null) return 0
  if (typeof v === 'number') return v
  if (typeof v === 'object' && 'value' in v) return Number(v.value)
  return Number(v)
}

export default client
