import client from './client'

export const listRooms = () => client.get('/Room/list').then((r) => r.data)
export const getRoom = (id: string) => client.get(`/Room/get/${id}`).then((r) => r.data)
export const createRoom = (data: { name: string; idTheater: string }) =>
  client.post('/Room/create', data).then((r) => r.data)
export const updateRoom = (data: { id: string; name: string; idTheater: string }) =>
  client.put('/Room/update', data).then((r) => r.data)
export const deleteRoom = (id: string) => client.delete(`/Room/delete/${id}`).then((r) => r.data)
