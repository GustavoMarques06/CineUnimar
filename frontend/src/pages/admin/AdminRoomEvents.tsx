import { useEffect, useState } from 'react'
import { Plus, Trash2 } from 'lucide-react'
import { listRoomEvents, createRoomEvent, deleteRoomEvent } from '../../api/roomEvents'
import { listRooms } from '../../api/rooms'
import { val } from '../../api/client'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

export default function AdminRoomEvents() {
  const [items, setItems] = useState<Item[]>([])
  const [rooms, setRooms] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState(false)
  const [form, setForm] = useState({ idRoom: '' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    Promise.all([listRoomEvents(), listRooms()])
      .then(([re, r]) => { setItems(re); setRooms(r) })
      .catch(() => {})
      .finally(() => setLoading(false))
  }

  useEffect(load, [])

  const roomName = (id: string) => {
    const r = rooms.find((ro: Item) => (val(ro.id) || ro.id) === id)
    return r ? val(r.name) : id?.slice(0, 8) ?? '–'
  }

  const openCreate = () => {
    setForm({ idRoom: rooms[0] ? (val(rooms[0].id) || rooms[0].id) : '' })
    setError(''); setModal(true)
  }

  const handleSave = async () => {
    setSaving(true); setError('')
    try {
      await createRoomEvent(form)
      setModal(false); load()
    } catch { setError('Erro ao salvar.') }
    finally { setSaving(false) }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir esta sala-evento?')) return
    try { await deleteRoomEvent(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Sala-Eventos</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Nova Sala-Evento
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">ID</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Sala</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Lotado?</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={4} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={4} className="px-4 py-8 text-center text-gray-400">Nenhuma sala-evento cadastrada</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-mono text-xs text-gray-400">{id.slice(0, 8)}</td>
                  <td className="px-4 py-3 text-gray-700">{roomName(val(item.idRoom) || item.idRoom)}</td>
                  <td className="px-4 py-3">
                    <span className={`text-xs px-2 py-0.5 rounded-full font-medium ${item.isFull ? 'bg-red-100 text-red-600' : 'bg-green-100 text-green-700'}`}>
                      {item.isFull ? 'Sim' : 'Não'}
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
        <Modal title="Nova Sala-Evento" onClose={() => setModal(false)}>
          <div className="space-y-4">
            <div>
              <label className="label">Sala</label>
              <select className="input" value={form.idRoom} onChange={(e) => setForm({ idRoom: e.target.value })}>
                <option value="">Selecione...</option>
                {rooms.map((r: Item) => {
                  const rid = val(r.id) || r.id
                  return <option key={rid} value={rid}>{val(r.name)}</option>
                })}
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
