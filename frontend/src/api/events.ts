import client from './client'

export const listEvents = () => client.get('/Events/list').then((r) => r.data)
export const getEvent = (id: string) => client.get(`/Events/get/${id}`).then((r) => r.data)
export const createEvent = (data: object) => client.post('/Events/create', data).then((r) => r.data)
export const updateEvent = (data: object) => client.put('/Events/update', data).then((r) => r.data)
export const deleteEvent = (id: string) => client.delete(`/Events/delete/${id}`).then((r) => r.data)
