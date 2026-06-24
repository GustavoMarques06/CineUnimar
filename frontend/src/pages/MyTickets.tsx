import { useEffect, useState } from 'react'
import { Ticket, Calendar, CreditCard } from 'lucide-react'
import { listMyTickets } from '../api/tickets'
import { val, numVal } from '../api/client'

// API returns paymentStatus as string: "Pending", "Approved", "Rejected"
const STATUS_INFO: Record<string, { label: string; color: string }> = {
  Pending:  { label: 'Aguardando pagamento', color: 'bg-yellow-100 text-yellow-700' },
  Approved: { label: 'Aprovado',             color: 'bg-green-100 text-green-700'  },
  Rejected: { label: 'Rejeitado',            color: 'bg-red-100 text-red-600'      },
}

export default function MyTickets() {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [tickets, setTickets] = useState<any[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    listMyTickets()
      .then((data) => setTickets(data))
      .catch(() => setTickets([]))
      .finally(() => setLoading(false))
  }, [])

  return (
    <main className="max-w-4xl mx-auto px-4 py-8">
      <div className="flex items-center gap-3 mb-6">
        <Ticket size={24} className="text-indigo-600" />
        <h1 className="text-2xl font-bold text-gray-900">Meus Ingressos</h1>
      </div>

      {loading ? (
        <div className="space-y-3">
          {[1, 2, 3].map((i) => (
            <div key={i} className="card h-24 animate-pulse bg-gray-100" />
          ))}
        </div>
      ) : tickets.length === 0 ? (
        <div className="text-center py-20 text-gray-400">
          <Ticket size={48} strokeWidth={1} className="mx-auto mb-3" />
          <p className="text-lg">Você ainda não tem ingressos</p>
          <p className="text-sm mt-1">Compre seu primeiro ingresso na página inicial</p>
        </div>
      ) : (
        <div className="space-y-3">
          {tickets.map((ticket) => {
            const id = val(ticket.id) || ticket.id
            const status: string = ticket.paymentStatus ?? ticket.PaymentStatus ?? 'Pending'
            const price = numVal(ticket.price)
            const date = new Date(ticket.purchaseDate ?? Date.now())
            const statusInfo = STATUS_INFO[status] ?? STATUS_INFO['Pending']

            return (
              <div key={id} className="card p-4 flex items-center gap-4">
                <div className="w-12 h-12 rounded-lg bg-indigo-100 flex items-center justify-center shrink-0">
                  <Ticket size={20} className="text-indigo-600" />
                </div>

                <div className="flex-1 min-w-0">
                  <div className="flex items-center gap-2 flex-wrap">
                    <p className="font-medium text-gray-900 text-sm truncate">
                      Ingresso #{id.slice(0, 8)}
                    </p>
                    <span className={`text-xs px-2 py-0.5 rounded-full font-medium ${statusInfo.color}`}>
                      {statusInfo.label}
                    </span>
                  </div>
                  <div className="flex items-center gap-4 mt-1 text-xs text-gray-500">
                    <span className="flex items-center gap-1">
                      <Calendar size={11} />
                      {date.toLocaleDateString('pt-BR')}
                    </span>
                    <span className="flex items-center gap-1">
                      <CreditCard size={11} />
                      R$ {price.toFixed(2).replace('.', ',')}
                    </span>
                  </div>
                </div>
              </div>
            )
          })}
        </div>
      )}
    </main>
  )
}
