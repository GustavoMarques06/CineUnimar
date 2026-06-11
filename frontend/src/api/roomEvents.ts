import client from './client'

export const listRoomEvents = () => client.get('/RoomEvent/list').then((r) => r.data)
export const getRoomEvent = (id: string) =>
  client.get(`/RoomEvent/get/${id}`).then((r) => r.data)
export const createRoomEvent = (data: { idRoom: string }) =>
  client.post('/RoomEvent/create', data).then((r) => r.data)
export const updateRoomEvent = (data: { id: string; idRoom: string; isFull: boolean }) =>
  client.put('/RoomEvent/update', data).then((r) => r.data)
export const deleteRoomEvent = (id: string) =>
  client.delete(`/RoomEvent/delete/${id}`).then((r) => r.data)
