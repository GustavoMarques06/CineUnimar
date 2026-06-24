import { useEffect, useState } from 'react'
import { Plus, Pencil, Trash2 } from 'lucide-react'
import { listEvents, createEvent, updateEvent, deleteEvent } from '../../api/events'
import { listRooms } from '../../api/rooms'
import { val, numVal } from '../../api/client'
import { useAuth } from '../../contexts/AuthContext'
import { EventStatus } from '../../types'
import Modal from '../../components/Modal'

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type Item = any

const STATUS_LABEL: Record<number, string> = {
  [EventStatus.Pending]: 'Em breve',
  [EventStatus.Occurring]: 'Acontecendo',
  [EventStatus.Ended]: 'Encerrado',
  [EventStatus.Cancelled]: 'Cancelado',
}

const EMPTY_GUID = '00000000-0000-0000-0000-000000000000'

const emptyForm = {
  name: '',
  description: '',
  date: '',
  duration: '120',
  roomId: '',
  status: String(EventStatus.Pending),
  categoryId: EMPTY_GUID,
  userCreatorId: '',
}

export default function AdminEvents() {
  const { user } = useAuth()
  const userId = user?.userId
  const [items, setItems] = useState<Item[]>([])
  const [rooms, setRooms] = useState<Item[]>([])
  const [loading, setLoading] = useState(true)
  const [modal, setModal] = useState<'create' | 'edit' | null>(null)
  const [selected, setSelected] = useState<Item | null>(null)
  const [form, setForm] = useState(emptyForm)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')

  const load = () => {
    setLoading(true)
    Promise.all([listEvents(), listRooms()])
      .then(([e, r]) => { setItems(e); setRooms(r) })
      .catch(() => {})
      .finally(() => setLoading(false))
  }

  useEffect(load, [])

  const roomName = (id: string) => {
    const r = rooms.find((ro: Item) => (val(ro.id) || ro.id) === id)
    return r ? val(r.name) : id?.slice(0, 8) ?? '–'
  }

  const set = (field: string) => (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) =>
    setForm((f) => ({ ...f, [field]: e.target.value }))

  const openCreate = () => {
    setForm({ ...emptyForm, roomId: rooms[0] ? (val(rooms[0].id) || rooms[0].id) : '', userCreatorId: userId ?? EMPTY_GUID })
    setSelected(null); setError(''); setModal('create')
  }

  const openEdit = (item: Item) => {
    const date = val(item.date) || item.date
    const localDate = date ? new Date(date).toISOString().slice(0, 16) : ''
    setForm({
      name: val(item.name),
      description: val(item.description),
      date: localDate,
      duration: String(numVal(item.duration)),
      roomId: val(item.roomId) || item.roomId,
      status: String(numVal(item.status)),
      categoryId: val(item.categoryId) || item.categoryId || EMPTY_GUID,
      userCreatorId: val(item.userCreatorId) || item.userCreatorId || userId || EMPTY_GUID,
    })
    setSelected(item); setError(''); setModal('edit')
  }

  const handleSave = async () => {
    setSaving(true); setError('')
    try {
      const payload = {
        name: form.name,
        description: form.description || null,
        date: new Date(form.date).toISOString(),
        duration: parseInt(form.duration),
        roomId: form.roomId,
        status: parseInt(form.status),
        categoryId: form.categoryId || EMPTY_GUID,
        userCreatorId: form.userCreatorId || userId || EMPTY_GUID,
      }
      if (modal === 'create') await createEvent(payload)
      else await updateEvent({ id: val(selected.id) || selected.id, ...payload })
      setModal(null); load()
    } catch (err: unknown) {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      const data = (err as any)?.response?.data
      const msg = typeof data === 'string' ? data : data?.message ?? data?.errors ?? 'Erro ao salvar.'
      setError(typeof msg === 'string' ? msg : JSON.stringify(msg))
    }
    finally { setSaving(false) }
  }

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir este evento?')) return
    try { await deleteEvent(id); load() } catch { alert('Erro ao excluir.') }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl font-bold text-gray-900">Eventos</h1>
        <button onClick={openCreate} className="btn-primary flex items-center gap-1.5">
          <Plus size={16} /> Novo Evento
        </button>
      </div>

      <div className="card overflow-hidden">
        <table className="w-full text-sm">
          <thead className="bg-gray-50 border-b border-gray-100">
            <tr>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Nome</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Data</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Sala</th>
              <th className="text-left px-4 py-3 font-medium text-gray-600">Status</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {loading ? (
              <tr><td colSpan={5} className="px-4 py-8 text-center text-gray-400">Carregando...</td></tr>
            ) : items.length === 0 ? (
              <tr><td colSpan={5} className="px-4 py-8 text-center text-gray-400">Nenhum evento cadastrado</td></tr>
            ) : items.map((item: Item) => {
              const id = val(item.id) || item.id
              const status = numVal(item.status)
              const date = new Date(val(item.date) || item.date)
              return (
                <tr key={id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 font-medium text-gray-900">{val(item.name)}</td>
                  <td className="px-4 py-3 text-gray-500">{date.toLocaleDateString('pt-BR')}</td>
                  <td className="px-4 py-3 text-gray-500">{roomName(val(item.roomId) || item.roomId)}</td>
                  <td className="px-4 py-3 text-gray-500">{STATUS_LABEL[status] ?? status}</td>
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
        <Modal title={modal === 'create' ? 'Novo Evento' : 'Editar Evento'} onClose={() => setModal(null)}>
          <div className="space-y-4">
            <div>
              <label className="label">Nome</label>
              <input className="input" value={form.name} onChange={set('name')} />
            </div>
            <div>
              <label className="label">Descrição</label>
              <textarea className="input" rows={2} value={form.description} onChange={set('description')} />
            </div>
            <div className="grid grid-cols-2 gap-3">
              <div>
                <label className="label">Data e hora</label>
                <input type="datetime-local" className="input" value={form.date} onChange={set('date')} />
              </div>
              <div>
                <label className="label">Duração (min)</label>
                <input type="number" className="input" value={form.duration} onChange={set('duration')} />
              </div>
            </div>
            <div>
              <label className="label">Sala</label>
              <select className="input" value={form.roomId} onChange={set('roomId')}>
                <option value="">Selecione...</option>
                {rooms.map((r: Item) => {
                  const rid = val(r.id) || r.id
                  return <option key={rid} value={rid}>{val(r.name)}</option>
                })}
              </select>
            </div>
            <div>
              <label className="label">Status</label>
              <select className="input" value={form.status} onChange={set('status')}>
                {Object.entries(STATUS_LABEL).map(([k, v]) => (
                  <option key={k} value={k}>{v}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="label">ID do Criador (GUID)</label>
              <input className="input font-mono text-xs" value={form.userCreatorId} onChange={set('userCreatorId')} placeholder={EMPTY_GUID} />
              <p className="text-xs text-gray-400 mt-1">Preenchido automaticamente com seu ID de usuário</p>
            </div>
            <div>
              <label className="label">ID da Categoria (GUID)</label>
              <input className="input font-mono text-xs" value={form.categoryId} onChange={set('categoryId')} placeholder={EMPTY_GUID} />
            </div>
            {error && <p className="text-sm text-red-600 bg-red-50 px-3 py-2 rounded-lg">{error}</p>}
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
