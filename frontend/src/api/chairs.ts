import client from './client'

export const listChairs = () => client.get('/Chair/list').then((r) => r.data)
export const getChair = (id: string) => client.get(`/Chair/get/${id}`).then((r) => r.data)
export const createChair = (data: { chairPosition: string; idRoom: string }) =>
  client.post('/Chair/create', data).then((r) => r.data)
export const updateChair = (data: { id: string; chairPosition: string; idRoom: string }) =>
  client.put('/Chair/update', data).then((r) => r.data)
export const deleteChair = (id: string) =>
  client.delete(`/Chair/delete/${id}`).then((r) => r.data)
