import { useState, type FormEvent } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Clapperboard } from 'lucide-react'
import { register } from '../api/auth'

export default function Register() {
  const navigate = useNavigate()
  const [form, setForm] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    cpf: '',
    dateOfBirth: '',
  })
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const set = (field: string) => (e: React.ChangeEvent<HTMLInputElement>) =>
    setForm((prev) => ({ ...prev, [field]: e.target.value }))

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      await register({ ...form, role: 2 }) // UserRole.User = 2
      navigate('/login')
    } catch (err: unknown) {
      const msg =
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (err as any)?.response?.data?.message ||
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        (err as any)?.response?.data ||
        'Erro ao cadastrar. Verifique os dados.'
      setError(typeof msg === 'string' ? msg : 'Erro ao cadastrar.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4 py-8">
      <div className="w-full max-w-sm">
        <div className="text-center mb-8">
          <Link to="/" className="inline-flex items-center gap-2 text-indigo-600 font-bold text-2xl">
            <Clapperboard size={28} />
            UnimarTickets
          </Link>
          <p className="text-gray-500 mt-2 text-sm">Crie sua conta</p>
        </div>

        <div className="card p-6">
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid grid-cols-2 gap-3">
              <div>
                <label className="label">Nome</label>
                <input className="input" value={form.firstName} onChange={set('firstName')} required />
              </div>
              <div>
                <label className="label">Sobrenome</label>
                <input className="input" value={form.lastName} onChange={set('lastName')} required />
              </div>
            </div>

            <div>
              <label className="label">E-mail</label>
              <input
                type="email"
                className="input"
                value={form.email}
                onChange={set('email')}
                placeholder="seu@email.com"
                required
              />
            </div>

            <div>
              <label className="label">Senha</label>
              <input
                type="password"
                className="input"
                value={form.password}
                onChange={set('password')}
                placeholder="Mínimo 8 caracteres"
                required
              />
            </div>

            <div>
              <label className="label">CPF</label>
              <input
                className="input"
                value={form.cpf}
                onChange={set('cpf')}
                placeholder="000.000.000-00"
                required
              />
            </div>

            <div>
              <label className="label">Data de Nascimento</label>
              <input
                type="date"
                className="input"
                value={form.dateOfBirth}
                onChange={set('dateOfBirth')}
                required
              />
            </div>

            {error && (
              <p className="text-sm text-red-600 bg-red-50 px-3 py-2 rounded-lg">{error}</p>
            )}

            <button type="submit" disabled={loading} className="btn-primary w-full">
              {loading ? 'Cadastrando...' : 'Criar conta'}
            </button>
          </form>
        </div>

        <p className="text-center text-sm text-gray-500 mt-4">
          Já tem conta?{' '}
          <Link to="/login" className="text-indigo-600 font-medium hover:underline">
            Entrar
          </Link>
        </p>
      </div>
    </div>
  )
}
