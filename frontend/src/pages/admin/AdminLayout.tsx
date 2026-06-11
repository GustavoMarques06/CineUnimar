import { Outlet, NavLink, Link } from 'react-router-dom'
import {
  Clapperboard,
  LayoutDashboard,
  Building2,
  DoorOpen,
  Armchair,
  CalendarDays,
  Ticket,
  CalendarRange,
  Grid3x3,
} from 'lucide-react'

const NAV = [
  { to: '/admin', label: 'Dashboard', icon: LayoutDashboard, end: true },
  { to: '/admin/cinemas', label: 'Cinemas', icon: Building2 },
  { to: '/admin/salas', label: 'Salas', icon: DoorOpen },
  { to: '/admin/cadeiras', label: 'Cadeiras', icon: Armchair },
  { to: '/admin/eventos', label: 'Eventos', icon: CalendarDays },
  { to: '/admin/sala-eventos', label: 'Sala-Eventos', icon: CalendarRange },
  { to: '/admin/cadeiras-evento', label: 'Cadeiras do Evento', icon: Grid3x3 },
  { to: '/admin/ingressos', label: 'Ingressos', icon: Ticket },
]

export default function AdminLayout() {
  return (
    <div className="flex min-h-screen bg-gray-50">
      {/* Sidebar */}
      <aside className="w-60 shrink-0 bg-white border-r border-gray-200 flex flex-col">
        <Link to="/" className="flex items-center gap-2 px-5 py-4 border-b border-gray-100 font-bold text-indigo-600">
          <Clapperboard size={20} />
          UnimarTickets Admin
        </Link>

        <nav className="flex-1 p-3 space-y-1">
          {NAV.map(({ to, label, icon: Icon, end }) => (
            <NavLink
              key={to}
              to={to}
              end={end}
              className={({ isActive }) =>
                `flex items-center gap-2.5 px-3 py-2 rounded-lg text-sm font-medium transition-colors ${
                  isActive
                    ? 'bg-indigo-50 text-indigo-700'
                    : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
                }`
              }
            >
              <Icon size={16} />
              {label}
            </NavLink>
          ))}
        </nav>

        <div className="p-3 border-t border-gray-100">
          <Link to="/" className="flex items-center gap-2 px-3 py-2 rounded-lg text-sm text-gray-500 hover:bg-gray-50 transition-colors">
            ← Voltar ao site
          </Link>
        </div>
      </aside>

      {/* Main */}
      <main className="flex-1 p-6 overflow-auto">
        <Outlet />
      </main>
    </div>
  )
}
