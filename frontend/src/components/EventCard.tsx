import { Link } from 'react-router-dom'
import { Calendar, Clock, Tag } from 'lucide-react'
import { val, numVal } from '../api/client'
import { EventStatus } from '../types'

const STATUS_LABEL: Record<number, { label: string; color: string }> = {
  [EventStatus.Pending]: { label: 'Em breve', color: 'bg-blue-100 text-blue-700' },
  [EventStatus.Occurring]: { label: 'Acontecendo', color: 'bg-green-100 text-green-700' },
  [EventStatus.Ended]: { label: 'Encerrado', color: 'bg-gray-100 text-gray-500' },
  [EventStatus.Cancelled]: { label: 'Cancelado', color: 'bg-red-100 text-red-600' },
}

const GRADIENTS = [
  'from-indigo-500 to-purple-600',
  'from-sky-500 to-indigo-600',
  'from-violet-500 to-pink-600',
  'from-emerald-500 to-teal-600',
  'from-orange-500 to-red-600',
  'from-pink-500 to-rose-600',
]

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export default function EventCard({ event }: { event: any }) {
  const name = val(event.name)
  const description = val(event.description)
  const status = numVal(event.status)
  const duration = numVal(event.duration)
  const date = new Date(val(event.date) || event.date)
  const id = val(event.id) || event.id

  const gradient = GRADIENTS[id.charCodeAt(0) % GRADIENTS.length]
  const statusInfo = STATUS_LABEL[status] ?? STATUS_LABEL[EventStatus.Pending]
  const isAvailable = status === EventStatus.Pending || status === EventStatus.Occurring

  return (
    <div className="card overflow-hidden flex flex-col group hover:shadow-md transition-shadow">
      <div className={`h-40 bg-gradient-to-br ${gradient} flex items-center justify-center`}>
        <span className="text-5xl">🎬</span>
      </div>

      <div className="p-4 flex flex-col gap-3 flex-1">
        <div className="flex items-start justify-between gap-2">
          <h3 className="font-semibold text-gray-900 text-base leading-tight line-clamp-2">
            {name || 'Sem título'}
          </h3>
          <span className={`shrink-0 text-xs px-2 py-0.5 rounded-full font-medium ${statusInfo.color}`}>
            {statusInfo.label}
          </span>
        </div>

        {description && (
          <p className="text-sm text-gray-500 line-clamp-2">{description}</p>
        )}

        <div className="flex flex-col gap-1.5 text-xs text-gray-500 mt-auto">
          <div className="flex items-center gap-1.5">
            <Calendar size={13} />
            <span>{date.toLocaleDateString('pt-BR', { dateStyle: 'long' })}</span>
          </div>
          <div className="flex items-center gap-1.5">
            <Clock size={13} />
            <span>
              {date.toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })} •{' '}
              {duration} min
            </span>
          </div>
          <div className="flex items-center gap-1.5">
            <Tag size={13} />
            <span className="font-semibold text-indigo-600">R$ 35,00</span>
          </div>
        </div>

        {isAvailable ? (
          <Link
            to={`/eventos/${id}`}
            className="btn-primary text-center text-sm mt-2"
          >
            Comprar Ingresso
          </Link>
        ) : (
          <button disabled className="btn-primary text-sm mt-2 opacity-40 cursor-not-allowed">
            Indisponível
          </button>
        )}
      </div>
    </div>
  )
}
