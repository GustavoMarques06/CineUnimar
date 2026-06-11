import { useEffect, useState } from 'react'
import { Plus, Pencil, Trash2 } from 'lucide-react'
import { listRooms, createRoom, updateRoom, deleteRoom } from '../../api/rooms'
import { listTheaters } from '../../api/theaters'
import { val } from '../../api/client'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

export default function AdminRooms() {
  const [items, setItems] = useState<Item[]>([])
  const [theaters, setTheaters] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState<'create' | 'edit' | null>(null)
  const [selected, setSelected] = useState<Item | null>(null)
  const [form, setForm] = useState({ name: '', idTheater: '' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    Promise.all([listRooms(), listTheaters()])
      .then(([r, t]) => { setItems(r); setTheaters(t) })
      .catch(() => {})
      .finally(() => setLoading(false))
  }

  useEffect(load, [])

  const theaterName = (id: string) => {
    const t = theaters.find((th: Item) => (val(th.id) || th.id) === id)
    return t ? val(t.name) : id.slice(0, 8)
  }

  const openCreate = () => {
    setForm({ name: '', idTheater: theaters[0] ? (val(theaters[0].id) || theaters[0].id) : '' })
    setSelected(null); setError(''); setModal('create')
  }

  const openEdit = (item: Item) => {
    setForm({ name: val(item.name), idTheater: val(item.idTheater) || item.idTheater })
    setSelected(item); setError(''); setModal('edit')
  }

  const handleSave = async () => {
    setSaving(true); setError('')
    try {
      if (modal === 'create') await createRoom(form)
      else await updateRoom({ id: val(selected.id) || selected.id, ...form })
      setModal(null); load()
    } catch { setError('Erro ao salvar.') }
    finally { setSaving(false) }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir esta sala?')) return
    try { await deleteRoom(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Salas</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Nova Sala
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Nome</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Cinema</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Nenhuma sala cadastrada</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-medium text-gray-900">{val(item.name)}</td>
                  <td className="px-4 py-3 text-gray-500">{theaterName(val(item.idTheater) || item.idTheater)}</td>
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
        <Modal title={modal === 'create' ? 'Nova Sala' : 'Editar Sala'} onClose={() => setModal(null)}>
          <div className="space-y-4">
            <div>
              <label className="label">Nome</label>
              <input className="input" value={form.name} onChange={(e) => setForm((f) => ({ ...f, name: e.target.value }))} />
            </div>
            <div>
              <label className="label">Cinema</label>
              <select className="input" value={form.idTheater} onChange={(e) => setForm((f) => ({ ...f, idTheater: e.target.value }))}>
                <option value="">Selecione...</option>
                {theaters.map((t: Item) => {
                  const tid = val(t.id) || t.id
                  return <option key={tid} value={tid}>{val(t.name)}</option>
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
