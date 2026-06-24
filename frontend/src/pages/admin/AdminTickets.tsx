import { useEffect, useState } from 'react'
import { Trash2, RefreshCw } from 'lucide-react'
import { listTickets, approvePayment, rejectPayment, deleteTicket } from '../../api/tickets'
import { val, numVal } from '../../api/client'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

// API returns paymentStatus as a string: "Pending", "Approved", "Rejected"
const STATUS_INFO: Record<string, { label: string; color: string }> = {
  Pending:  { label: 'Pendente',  color: 'text-yellow-700 bg-yellow-50 border-yellow-200' },
  Approved: { label: 'Aprovado',  color: 'text-green-700  bg-green-50  border-green-200'  },
  Rejected: { label: 'Rejeitado', color: 'text-red-600    bg-red-50    border-red-200'    },
}

function getStatus(item: Item): string {
  const raw = item.paymentStatus ?? item.PaymentStatus ?? ''
  return typeof raw === 'string' ? raw : String(raw)
}

export default function AdminTickets() {
  const [items, setItems] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [updating, setUpdating] = useState<string | null>(null)

  const load = () => {
    setLoading(true)
    listTickets().then(setItems).catch(() => setItems([])).finally(() => setLoading(false))
  }

  useEffect(load, [])

  const handleStatusChange = async (id: string, newStatus: string) => {
    setUpdating(id)
    try {
      if (newStatus === 'Approved') await approvePayment(id)
      else if (newStatus === 'Rejected') await rejectPayment(id)
      load()
    } catch {
      alert('Erro ao atualizar pagamento.')
    } finally {
      setUpdating(null)
    }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir este ingresso?')) return
    try { await deleteTicket(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Ingressos</h1>
        <div className="flex items-center gap-3">
          <span className="text-sm text-gray-500">{items.length} total</span>
          <button onClick={load} className="btn-secondary flex items-center gap-1.5 text-sm">
            <RefreshCw size={14} /> Atualizar
          </button>
        </div>
      </div>

      <div className="card overflow-x-auto">
        <table className="w-full text-sm min-w-[620px]">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">ID</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Usuário</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Valor</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Data</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Pagamento</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={6} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={6} className="px-4 py-8 text-center text-gray-400">Nenhum ingresso</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              const price = numVal(item.price)
              const status = getStatus(item)
              const statusInfo = STATUS_INFO[status] ?? STATUS_INFO['Pending']
              const date = new Date(item.purchaseDate ?? Date.now())
              const isUpdating = updating === id

              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-mono text-xs text-gray-400">{id.slice(0, 8)}</td>
                  <td className="px-4 py-3 text-gray-700 truncate max-w-[140px] font-mono text-xs">
                    {String(val(item.userId) || item.userId).slice(0, 13)}…
                  </td>
                  <td className="px-4 py-3 font-medium text-gray-900">
                    R$ {price.toFixed(2).replace('.', ',')}
                  </td>
                  <td className="px-4 py-3 text-gray-500">
                    {date.toLocaleDateString('pt-BR')}
                  </td>
                  <td className="px-4 py-3">
                    <select
                      value={status}
                      disabled={isUpdating || status === 'Approved' || status === 'Rejected'}
                      onChange={(e) => handleStatusChange(id, e.target.value)}
                      className={`text-xs font-medium border rounded-lg px-2 py-1.5 cursor-pointer focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:cursor-default ${statusInfo.color}`}
                    >
                      <option value="Pending">Pendente</option>
                      <option value="Approved">Aprovado</option>
                      <option value="Rejected">Rejeitado</option>
                    </select>
                    {isUpdating && (
                      <span className="ml-2 text-xs text-gray-400">salvando…</span>
                    )}
                  </td>
                  <td className="px-4 py-3 text-right">
                    <button
                      onClick={() => handleDelete(id)}
                      className="p-1.5 text-gray-400 hover:text-red-500 transition-colors"
                    >
                      <Trash2 size={15} />
                    </button>
                  </td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}
