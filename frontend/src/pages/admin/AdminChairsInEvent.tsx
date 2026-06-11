import { useEffect, useState } from 'react'
import { Plus, Trash2 } from 'lucide-react'
import { listChairsInEvent, createChairInEvent, deleteChairInEvent } from '../../api/chairsInEvent'
import { listRoomEvents } from '../../api/roomEvents'
import { listRooms } from '../../api/rooms'
import { val, numVal } from '../../api/client'
import { ChairStatus } from '../../types'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

const STATUS_LABEL = { [ChairStatus.Vacant]: 'Livre', [ChairStatus.Occupied]: 'Ocupado' }

export default function AdminChairsInEvent() {
  const [items, setItems] = useState<Item[]>([])
  const [roomEvents, setRoomEvents] = useState<Item[]>([])
  const [rooms, setRooms] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState(false)
  const [form, setForm] = useState({ idRoomEvent: '', status: String(ChairStatus.Vacant), quantity: '1' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    Promise.all([listChairsInEvent(), listRoomEvents(), listRooms()])
      .then(([c, re, r]) => { setItems(c); setRoomEvents(re); setRooms(r) })
      .catch(() => {})
      .finally(() => setLoading(false))
  }

  useEffect(load, [])

  const roomEventLabel = (id: string) => {
    const re = roomEvents.find((r: Item) => (val(r.id) || r.id) === id)
    if (!re) return id?.slice(0, 8) ?? '–'
    const room = rooms.find((r: Item) => (val(r.id) || r.id) === (val(re.idRoom) || re.idRoom))
    return room ? `${val(room.name)} (${id.slice(0, 6)})` : id.slice(0, 8)
  }

  const openCreate = () => {
    setForm({ idRoomEvent: roomEvents[0] ? (val(roomEvents[0].id) || roomEvents[0].id) : '', status: String(ChairStatus.Vacant), quantity: '1' })
    setError(''); setModal(true)
  }

  const handleSave = async () => {
    setSaving(true); setError('')
    try {
      const qty = Math.max(1, parseInt(form.quantity) || 1)
      for (let i = 0; i < qty; i++) {
        await createChairInEvent({ idRoomEvent: form.idRoomEvent, status: parseInt(form.status) })
      }
      setModal(false); load()
    } catch { setError('Erro ao salvar.') }
    finally { setSaving(false) }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir esta cadeira do evento?')) return
    try { await deleteChairInEvent(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Cadeiras do Evento</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Adicionar Cadeiras
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">ID</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Sala-Evento</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Status</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={4} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={4} className="px-4 py-8 text-center text-gray-400">Nenhuma cadeira de evento cadastrada</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              const status = numVal(item.status)
              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-mono text-xs text-gray-400">{id.slice(0, 8)}</td>
                  <td className="px-4 py-3 text-gray-700">{roomEventLabel(val(item.idRoomEvent) || item.idRoomEvent)}</td>
                  <td className="px-4 py-3">
                    <span className={`text-xs px-2 py-0.5 rounded-full font-medium ${status === ChairStatus.Occupied ? 'bg-red-100 text-red-600' : 'bg-green-100 text-green-700'}`}>
                      {STATUS_LABEL[status as ChairStatus] ?? status}
                    </span>
                  </td>
                  <td className="px-4 py-3 flex items-center justify-end">
                    <button onClick={() => handleDelete(id)} className="p-1.5 text-gray-400 hover:text-red-500 transition-colors"><Trash2 size={15} /></button>
                  </td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>

      {modal && (
        <Modal title="Adicionar Cadeiras ao Evento" onClose={() => setModal(false)}>
          <div className="space-y-4">
            <div>
              <label className="label">Sala-Evento</label>
              <select className="input" value={form.idRoomEvent} onChange={(e) => setForm((f) => ({ ...f, idRoomEvent: e.target.value }))}>
                <option value="">Selecione...</option>
                {roomEvents.map((re: Item) => {
                  const reid = val(re.id) || re.id
                  return <option key={reid} value={reid}>{roomEventLabel(reid)}</option>
                })}
              </select>
            </div>
            <div>
              <label className="label">Quantidade de cadeiras a criar</label>
              <input type="number" min="1" max="100" className="input" value={form.quantity} onChange={(e) => setForm((f) => ({ ...f, quantity: e.target.value }))} />
            </div>
            <div>
              <label className="label">Status inicial</label>
              <select className="input" value={form.status} onChange={(e) => setForm((f) => ({ ...f, status: e.target.value }))}>
                <option value={String(ChairStatus.Vacant)}>Livre</option>
                <option value={String(ChairStatus.Occupied)}>Ocupado</option>
              </select>
            </div>
            {error && <p className="text-sm text-red-600">{error}</p>}
            <div className="flex justify-end gap-2">
              <button onClick={() => setModal(false)} className="btn-secondary">Cancelar</button>
              <button onClick={handleSave} disabled={saving} className="btn-primary">{saving ? 'Salvando...' : 'Salvar'}</button>
            </div>
          </div>
        </Modal>
      )}
    </div>
  )
}
