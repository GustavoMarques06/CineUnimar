import { Link, useNavigate } from 'react-router-dom'
import { Clapperboard, Ticket, LogOut, User, ShieldCheck } from 'lucide-react'
import { useAuth } from '../contexts/AuthContext'

export default function Navbar() {
  const { user, isAdmin, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/')
  }

  return (
    <nav className="bg-white border-b border-gray-200 sticky top-0 z-40">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          <Link to="/" className="flex items-center gap-2 font-bold text-xl text-indigo-600">
            <Clapperboard size={24} />
            UnimarTickets
          </Link>

          <div className="flex items-center gap-4">
            {user ? (
              <>
                <Link
                  to="/meus-ingressos"
                  className="flex items-center gap-1.5 text-sm text-gray-600 hover:text-indigo-600 transition-colors"
                >
                  <Ticket size={16} />
                  Meus Ingressos
                </Link>

                {isAdmin && (
                  <Link
                    to="/admin"
                    className="flex items-center gap-1.5 text-sm text-indigo-600 font-medium hover:text-indigo-700 transition-colors"
                  >
                    <ShieldCheck size={16} />
                    Admin
                  </Link>
                )}

                <div className="flex items-center gap-2 text-sm text-gray-700">
                  <User size={16} />
                  <span className="hidden sm:inline">{user.userName}</span>
                </div>

                <button
                  onClick={handleLogout}
                  className="flex items-center gap-1.5 text-sm text-gray-500 hover:text-red-500 transition-colors"
                >
                  <LogOut size={16} />
                  <span className="hidden sm:inline">Sair</span>
                </button>
              </>
            ) : (
              <>
                <Link
                  to="/login"
                  className="text-sm text-gray-600 hover:text-indigo-600 transition-colors"
                >
                  Entrar
                </Link>
                <Link to="/cadastro" className="btn-primary text-sm">
                  Cadastrar
                </Link>
              </>
            )}
          </div>
        </div>
      </div>
    </nav>
  )
}
