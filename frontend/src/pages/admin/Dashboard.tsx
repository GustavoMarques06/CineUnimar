import { useEffect, useState } from 'react'
import { Building2, DoorOpen, CalendarDays, Ticket } from 'lucide-react'
import { listTheaters } from '../../api/theaters'
import { listRooms } from '../../api/rooms'
import { listEvents } from '../../api/events'
import { listTickets } from '../../api/tickets'

interface StatCard {
  label: string
  value: number
  icon: React.ReactNode
  color: string
}

export default function Dashboard() {
  const [stats, setStats] = useState({ theaters: 0, rooms: 0, events: 0, tickets: 0 })
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    Promise.allSettled([listTheaters(), listRooms(), listEvents(), listTickets()]).then(
      ([t, r, e, tk]) => {
        setStats({
          theaters: t.status === 'fulfilled' ? t.value.length : 0,
          rooms: r.status === 'fulfilled' ? r.value.length : 0,
          events: e.status === 'fulfilled' ? e.value.length : 0,
          tickets: tk.status === 'fulfilled' ? tk.value.length : 0,
        })
        setLoading(false)
      }
    )
  }, [])

  const cards: StatCard[] = [
    { label: 'Cinemas', value: stats.theaters, icon: <Building2 size={22} />, color: 'text-indigo-600 bg-indigo-50' },
    { label: 'Salas', value: stats.rooms, icon: <DoorOpen size={22} />, color: 'text-purple-600 bg-purple-50' },
    { label: 'Eventos', value: stats.events, icon: <CalendarDays size={22} />, color: 'text-sky-600 bg-sky-50' },
    { label: 'Ingressos', value: stats.tickets, icon: <Ticket size={22} />, color: 'text-emerald-600 bg-emerald-50' },
  ]

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-900 mb-6">Dashboard</h1>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {cards.map(({ label, value, icon, color }) => (
          <div key={label} className="card p-5">
            <div className={`w-10 h-10 rounded-lg flex items-center justify-center mb-3 ${color}`}>
              {icon}
            </div>
            <p className="text-2xl font-bold text-gray-900">
              {loading ? '–' : value}
            </p>
            <p className="text-sm text-gray-500 mt-0.5">{label}</p>
          </div>
        ))}
      </div>

      <div className="card p-6 mt-6">
        <h2 className="font-semibold text-gray-800 mb-2">Guia rápido</h2>
        <ol className="text-sm text-gray-500 space-y-1.5 list-decimal list-inside">
          <li>Crie um <strong>Cinema</strong> (aba Cinemas)</li>
          <li>Crie uma <strong>Sala</strong> vinculada ao cinema</li>
          <li>Cadastre as <strong>Cadeiras</strong> da sala (ex.: A1, A2...)</li>
          <li>Crie um <strong>Evento</strong> vinculado à sala</li>
          <li>Crie uma <strong>Sala-Evento</strong> para a sessão</li>
          <li>Cadastre as <strong>Cadeiras do Evento</strong> na sessão</li>
          <li>Usuários já podem comprar ingressos!</li>
        </ol>
      </div>
    </div>
  )
}
