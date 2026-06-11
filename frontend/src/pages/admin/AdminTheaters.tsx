import { useEffect, useState } from 'react'
import { Plus, Pencil, Trash2 } from 'lucide-react'
import { listTheaters, createTheater, updateTheater, deleteTheater } from '../../api/theaters'
import { val } from '../../api/client'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

export default function AdminTheaters() {
  const [items, setItems] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState<'create' | 'edit' | null>(null)
  const [selected, setSelected] = useState<Item | null>(null)
  const [form, setForm] = useState({ name: '', location: '' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    listTheaters().then(setItems).catch(() => setItems([])).finally(() => setLoading(false))
  }

  useEffect(load, [])

  const openCreate = () => {
    setForm({ name: '', location: '' })
    setSelected(null)
    setError('')
    setModal('create')
  }

  const openEdit = (item: Item) => {
    setForm({ name: val(item.name), location: val(item.location) })
    setSelected(item)
    setError('')
    setModal('edit')
  }

  const handleSave = async () => {
    setSaving(true)
    setError('')
    try {
      if (modal === 'create') {
        await createTheater(form)
      } else {
        await updateTheater({ id: val(selected.id) || selected.id, ...form })
      }
      setModal(null)
      load()
    } catch {
      setError('Erro ao salvar.')
    } finally {
      setSaving(false)
    }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir este cinema?')) return
    try {
      await deleteTheater(id)
      load()
    } catch {
      alert('Erro ao excluir.')
    }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Cinemas</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Novo Cinema
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Nome</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Localização</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={3} className="px-4 py-8 text-center text-gray-400">Nenhum cinema cadastrado</td></tr>
            ) : (
              items.map((item: Item) => {
                const id = val(item.id) || item.id
                return (
                  <tr key={id} className="hover:bg-gray-50">
                    <td className="px-4 py-3 font-medium text-gray-900">{val(item.name)}</td>
                    <td className="px-4 py-3 text-gray-500">{val(item.location)}</td>
                    <td className="px-4 py-3 flex items-center gap-2 justify-end">
                      <button onClick={() => openEdit(item)} className="p-1.5 text-gray-400 hover:text-indigo-600 transition-colors">
                        <Pencil size={15} />
                      </button>
                      <button onClick={() => handleDelete(id)} className="p-1.5 text-gray-400 hover:text-red-500 transition-colors">
                        <Trash2 size={15} />
                      </button>
                    </td>
                  </tr>
                )
              })
            )}
          </tbody>
        </table>
      </div>

      {modal && (
        <Modal title={modal === 'create' ? 'Novo Cinema' : 'Editar Cinema'} onClose={() => setModal(null)}>
          <div className="space-y-4">
            <div>
              <label className="label">Nome</label>
              <input className="input" value={form.name} onChange={(e) => setForm((f) => ({ ...f, name: e.target.value }))} />
            </div>
            <div>
              <label className="label">Localização</label>
              <input className="input" value={form.location} onChange={(e) => setForm((f) => ({ ...f, location: e.target.value }))} />
            </div>
            {error && <p className="text-sm text-red-600">{error}</p>}
            <div className="flex justify-end gap-2">
              <button onClick={() => setModal(null)} className="btn-secondary">Cancelar</button>
              <button onClick={handleSave} disabled={saving} className="btn-primary">
                {saving ? 'Salvando...' : 'Salvar'}
              </button>
            </div>
          </div>
        </Modal>
      )}
    </div>
  )
}
