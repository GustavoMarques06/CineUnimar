export interface LoginResponse {
  accessToken: string
  expiresAt: string
  userName: string
  role: string
}

export interface User {
  id: string
  firstName: string
  lastName: string
  email: string
  role: UserRole
  cpf: string
  dateOfBirth: string
}

export enum UserRole {
  Admin = 1,
  User = 2,
}

export interface Theater {
  id: string
  name: string
  location: string
}

export interface Room {
  id: string
  name: string
  idTheater: string
}

export interface Chair {
  id: string
  chairPosition: string
  idRoom: string
}

export interface RoomEvent {
  id: string
  idRoom: string
  isFull: boolean
}

export interface ChairInEvent {
  id: string
  idRoomEvent: string
  status: ChairStatus
}

export enum ChairStatus {
  Occupied = 1,
  Vacant = 2,
}

export interface Event {
  id: string
  name: string
  description?: string
  date: string
  duration: number
  roomId: string
  status: EventStatus
  categoryId: string
  userCreatorId: string
}

export enum EventStatus {
  Pending = 1,
  Occurring = 2,
  Ended = 3,
  Cancelled = 4,
}

export interface Ticket {
  id: string
  eventId: string
  userId: string
  chairInEventId: string
  price: number
  purchase_Data: string
  paymentStatus: PaymentStatus
}

export enum PaymentStatus {
  Pending = 1,
  Approved = 2,
  Rejected = 3,
}

export interface CreateTicketRequest {
  eventId: string
  userId: string
  chairInEventId: string
  price: number
  date: string
  paymentStatus: PaymentStatus
}

export interface SellTicketRequest {
  ticketId: string
  userId: string
  eventId: string
  chairInEventId: string
  price: number
}
