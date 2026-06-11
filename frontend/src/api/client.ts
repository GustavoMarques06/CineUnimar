import axios from 'axios'

const client = axios.create({ baseURL: '/api' })

client.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')
  if (token) config.headers.Authorization = `Bearer ${token}`
  return config
})

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
