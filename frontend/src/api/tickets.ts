import client from './client'
import type { SellTicketRequest } from '../types'

export const listTickets = () => client.get('/tickets').then((r) => r.data)
export const listMyTickets = () => client.get('/tickets/mine').then((r) => r.data)
export const getTicket = (id: string) => client.get(`/tickets/${id}`).then((r) => r.data)
export const sellTicket = (data: SellTicketRequest) =>
  client.post('/tickets/sell', data).then((r) => r.data)
export const approvePayment = (id: string) =>
  client.post(`/tickets/${id}/payment/approve`).then((r) => r.data)
export const rejectPayment = (id: string) =>
  client.post(`/tickets/${id}/payment/reject`).then((r) => r.data)
export const deleteTicket = (id: string) =>
  client.delete(`/tickets/${id}`).then((r) => r.data)
