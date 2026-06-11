import client from './client'

export const listTheaters = () => client.get('/Theater/list').then((r) => r.data)
export const getTheater = (id: string) => client.get(`/Theater/get/${id}`).then((r) => r.data)
export const createTheater = (data: { name: string; location: string }) =>
  client.post('/Theater/create', data).then((r) => r.data)
export const updateTheater = (data: { id: string; name: string; location: string }) =>
  client.put('/Theater/update', data).then((r) => r.data)
export const deleteTheater = (id: string) =>
  client.delete(`/Theater/delete/${id}`).then((r) => r.data)
