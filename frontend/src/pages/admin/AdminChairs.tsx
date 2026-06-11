import { useEffect, useState } from 'react'
import { Plus, Pencil, Trash2 } from 'lucide-react'
import { listChairs, createChair, updateChair, deleteChair } from '../../api/chairs'
import { listRooms } from '../../api/rooms'
import { val } from '../../api/client'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

export default function AdminChairs() {
  const [items, setItems] = useState<Item[]>([])
  const [rooms, setRooms] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState<'create' | 'edit' | null>(null)
  const [selected, setSelected] = useState<Item | null>(null)
  const [form, setForm] = useState({ chairPosition: '', idRoom: '' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    Promise.all([listChairs(), listRooms()])
      .then(([c, r]) => { setItems(c); setRooms(r) })
      .catch(() => {})
      .finally(() => setLoading(false))
  }

  useEffect(load, [])

  const roomName = (id: string) => {
    const r = rooms.find((ro: Item) => (val(ro.id) || ro.id) === id)
    return r ? val(r.name) : id.slice(0, 8)
  }

  const openCreate = () => {
    setForm({ chairPosition: '', idRoom: rooms[0] ? (val(rooms[0].id) || rooms[0].id) : '' })
    setSelected(null); setError(''); setModal('create')
  }

  const openEdit = (item: Item) => {
    setForm({ chairPosition: val(item.chairPosition), idRoom: val(item.idRoom) || item.idRoom })
    setSelected(item); setError(''); setModal('edit')
  }

  const handleSave = async () => {
    setSaving(true); setError('')
    try {
      if (modal === 'create') await createChair(form)
      else await updateChair({ id: val(selected.id) || selected.id, ...form })
      setModal(null); load()
    } catch { setError('Erro ao salvar.') }
    finally { setSaving(false) }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir esta cadeira?')) return
    try { await deleteChair(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Cadeiras</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Nova Cadeira
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Posição</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Sala</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Nenhuma cadeira cadastrada</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-medium text-gray-900">{val(item.chairPosition)}</td>
                  <td className="px-4 py-3 text-gray-500">{roomName(val(item.idRoom) || item.idRoom)}</td>
                  <td className="px-4 py-3 flex items-center gap-2 justify-end">
                    <button onClick={() => openEdit(item)} className="p-1.5 text-gray-400 hover:text-indigo-600 transition-colors"><Pencil size={15} /></button>
                    <button onClick={() => handleDelete(id)} className="p-1.5 text-gray-400 hover:text-red-500 transition-colors"><Trash2 size={15} /></button>
                  </td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>

      {modal && (
        <Modal title={modal === 'create' ? 'Nova Cadeira' : 'Editar Cadeira'} onClose={() => setModal(null)}>
          <div className="space-y-4">
            <div>
              <label className="label">Posição (ex: A1, B3)</label>
              <input className="input" value={form.chairPosition} placeholder="A1" onChange={(e) => setForm((f) => ({ ...f, chairPosition: e.target.value }))} />
            </div>
            <div>
              <label className="label">Sala</label>
              <select className="input" value={form.idRoom} onChange={(e) => setForm((f) => ({ ...f, idRoom: e.target.value }))}>
                <option value="">Selecione...</option>
                {rooms.map((r: Item) => {
                  const rid = val(r.id) || r.id
                  return <option key={rid} value={rid}>{val(r.name)}</option>
                })}
              </select>
            </div>
            {error && <p className="text-sm text-red-600">{error}</p>}
            <div className="flex justify-end gap-2">
              <button onClick={() => setModal(null)} className="btn-secondary">Cancelar</button>
              <button onClick={handleSave} disabled={saving} className="btn-primary">{saving ? 'Salvando...' : 'Salvar'}</button>
            </div>
          </div>
        </Modal>
      )}
    </div>
  )
}
