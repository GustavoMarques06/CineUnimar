import client from './client'
import type { Event } from '../types'

export type CreateEventRequest = Omit<Event, 'id' | 'userCreatorId'>
export type UpdateEventRequest = Pick<Event, 'id'> & Partial<CreateEventRequest>

export const listEvents = () => client.get('/Events/list').then((r) => r.data)
export const getEvent = (id: string) => client.get(`/Events/get/${id}`).then((r) => r.data)
export const createEvent = (data: CreateEventRequest) => client.post('/Events/create', data).then((r) => r.data)
export const updateEvent = (data: UpdateEventRequest) => client.put('/Events/update', data).then((r) => r.data)
export const deleteEvent = (id: string) => client.delete(`/Events/delete/${id}`).then((r) => r.data)
