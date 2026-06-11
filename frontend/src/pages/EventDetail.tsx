import { useEffect, useState } from 'react'
import { useParams, useNavigate, Link } from 'react-router-dom'
import { ArrowLeft, Calendar, Clock, MapPin, CheckCircle } from 'lucide-react'
import { getEvent } from '../api/events'
import { listRoomEvents } from '../api/roomEvents'
import { listChairsInEvent } from '../api/chairsInEvent'
import { sellTicket } from '../api/tickets'
import { val, numVal } from '../api/client'
import { useAuth } from '../contexts/AuthContext'
import SeatMap from '../components/SeatMap'

const PRICE = 35.0

export default function EventDetail() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { user, userId } = useAuth()

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [event, setEvent] = useState<any>(null)
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [seats, setSeats] = useState<any[]>([])
  const [selectedSeat, setSelectedSeat] = useState<string | null>(null)
  const [loading, setLoading] = useState(true)
  const [buying, setBuying] = useState(false)
  const [success, setSuccess] = useState(false)
  const [error, setError] = useState('')

  useEffect(() => {
    if (!id) return
    Promise.all([getEvent(id), listRoomEvents(), listChairsInEvent()])
      .then(([ev, roomEvents, chairsInEv]) => {
        setEvent(ev)
        const roomId = val(ev.roomId) || ev.roomId
        const roomEvent = roomEvents.find(
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          (re: any) => (val(re.idRoom) || re.idRoom) === roomId
        )
        if (roomEvent) {
          const reId = val(roomEvent.id) || roomEvent.id
          const filtered = chairsInEv.filter(
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            (c: any) => (val(c.idRoomEvent) || c.idRoomEvent) === reId
          )
          setSeats(filtered)
        }
      })
      .catch(() => setError('Erro ao carregar evento.'))
      .finally(() => setLoading(false))
  }, [id])

  const handleBuy = async () => {
    if (!selectedSeat) return
    if (!user || !userId) {
      navigate('/login')
      return
    }
    setError('')
    setBuying(true)
    try {
      // SellTicketUseCase already creates + validates the ticket internally
      await sellTicket({
        ticketId: '00000000-0000-0000-0000-000000000000', // ignored by backend
        userId,
        eventId: id!,
        chairInEventId: selectedSeat,
        price: PRICE,
      })
      setSuccess(true)
    } catch (err: unknown) {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      const data = (err as any)?.response?.data
      const msg = data?.Erro ?? data?.message ?? data ?? 'Erro ao comprar ingresso.'
      setError(typeof msg === 'string' ? msg : JSON.stringify(msg))
    } finally {
      setBuying(false)
    }
  }

  if (loading) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-8">
        <div className="h-8 w-48 bg-gray-100 rounded animate-pulse mb-6" />
        <div className="card h-96 animate-pulse bg-gray-100" />
      </div>
    )
  }

  if (!event) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-8 text-center text-gray-400">
        Evento não encontrado.{' '}
        <Link to="/" className="text-indigo-600">Voltar</Link>
      </div>
    )
  }

  if (success) {
    return (
      <div className="max-w-lg mx-auto px-4 py-20 text-center">
        <CheckCircle size={64} className="text-green-500 mx-auto mb-4" strokeWidth={1.5} />
        <h2 className="text-2xl font-bold text-gray-900 mb-2">Ingresso comprado!</h2>
        <p className="text-gray-500 mb-6">
          Seu ingresso está aguardando confirmação de pagamento.
        </p>
        <div className="flex gap-3 justify-center">
          <Link to="/meus-ingressos" className="btn-primary">Ver Meus Ingressos</Link>
          <Link to="/" className="btn-secondary">Voltar ao início</Link>
        </div>
      </div>
    )
  }

  const name = val(event.name)
  const description = val(event.description)
  const duration = numVal(event.duration)
  const date = new Date(val(event.date) || event.date)

  return (
    <main className="max-w-4xl mx-auto px-4 py-8">
      <button onClick={() => navigate(-1)} className="flex items-center gap-1.5 text-gray-500 hover:text-gray-700 text-sm mb-6 transition-colors">
        <ArrowLeft size={16} />
        Voltar
      </button>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Event info */}
        <div className="lg:col-span-1">
          <div className="card overflow-hidden sticky top-20">
            <div className="h-48 bg-gradient-to-br from-indigo-500 to-purple-600 flex items-center justify-center">
              <span className="text-6xl">🎬</span>
            </div>
            <div className="p-4 space-y-3">
              <h1 className="text-xl font-bold text-gray-900">{name}</h1>
              {description && <p className="text-sm text-gray-500">{description}</p>}
              <div className="space-y-2 text-sm text-gray-600">
                <div className="flex items-center gap-2">
                  <Calendar size={14} className="text-indigo-500" />
                  {date.toLocaleDateString('pt-BR', { dateStyle: 'full' })}
                </div>
                <div className="flex items-center gap-2">
                  <Clock size={14} className="text-indigo-500" />
                  {date.toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })} • {duration} min
                </div>
                <div className="flex items-center gap-2">
                  <MapPin size={14} className="text-indigo-500" />
                  Sala do evento
                </div>
              </div>
              <div className="border-t border-gray-100 pt-3">
                <p className="text-xs text-gray-400 mb-1">Valor do ingresso</p>
                <p className="text-2xl font-bold text-indigo-600">R$ {PRICE.toFixed(2).replace('.', ',')}</p>
              </div>
            </div>
          </div>
        </div>

        {/* Seat map */}
        <div className="lg:col-span-2">
          <div className="card p-6">
            <h2 className="text-lg font-semibold text-gray-900 mb-1">Escolha seu assento</h2>
            <p className="text-sm text-gray-500 mb-6">
              {seats.length} assento{seats.length !== 1 ? 's' : ''} disponível{seats.length !== 1 ? 'is' : ''}
            </p>

            <SeatMap seats={seats} selectedId={selectedSeat} onSelect={setSelectedSeat} />

            {error && (
              <p className="mt-4 text-sm text-red-600 bg-red-50 px-3 py-2 rounded-lg">{error}</p>
            )}

            <div className="mt-6 border-t border-gray-100 pt-4 flex items-center justify-between">
              <div>
                {selectedSeat ? (
                  <p className="text-sm text-gray-600">
                    Assento selecionado · <span className="font-semibold text-indigo-600">R$ {PRICE.toFixed(2).replace('.', ',')}</span>
                  </p>
                ) : (
                  <p className="text-sm text-gray-400">Selecione um assento para continuar</p>
                )}
              </div>
              <button
                onClick={handleBuy}
                disabled={!selectedSeat || buying}
                className="btn-primary"
              >
                {buying ? 'Processando...' : user ? 'Comprar Ingresso' : 'Entrar para comprar'}
              </button>
            </div>
          </div>
        </div>
      </div>
    </main>
  )
}
