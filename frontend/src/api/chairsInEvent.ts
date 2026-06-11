import client from './client'

export const listChairsInEvent = () => client.get('/ChairsInEvent/list').then((r) => r.data)
export const getChairInEvent = (id: string) =>
  client.get(`/ChairsInEvent/get/${id}`).then((r) => r.data)
export const createChairInEvent = (data: { idRoomEvent: string; status: number }) =>
  client.post('/ChairsInEvent/create', data).then((r) => r.data)
export const updateChairInEvent = (data: { id: string; idRoomEvent: string; status: number }) =>
  client.put('/ChairsInEvent/update', data).then((r) => r.data)
export const deleteChairInEvent = (id: string) =>
  client.delete(`/ChairsInEvent/delete/${id}`).then((r) => r.data)
