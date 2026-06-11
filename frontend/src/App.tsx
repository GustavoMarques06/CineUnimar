import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext'
import ProtectedRoute from './components/ProtectedRoute'
import Navbar from './components/Navbar'
import Home from './pages/Home'
import EventDetail from './pages/EventDetail'
import Login from './pages/Login'
import Register from './pages/Register'
import MyTickets from './pages/MyTickets'
import AdminLayout from './pages/admin/AdminLayout'
import Dashboard from './pages/admin/Dashboard'
import AdminTheaters from './pages/admin/AdminTheaters'
import AdminRooms from './pages/admin/AdminRooms'
import AdminChairs from './pages/admin/AdminChairs'
import AdminEvents from './pages/admin/AdminEvents'
import AdminTickets from './pages/admin/AdminTickets'
import AdminRoomEvents from './pages/admin/AdminRoomEvents'
import AdminChairsInEvent from './pages/admin/AdminChairsInEvent'

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          {/* Public routes */}
          <Route
            path="/"
            element={
              <>
                <Navbar />
                <Home />
              </>
            }
          />
          <Route
            path="/eventos/:id"
            element={
              <>
                <Navbar />
                <EventDetail />
              </>
            }
          />
          <Route path="/login" element={<Login />} />
          <Route path="/cadastro" element={<Register />} />
          <Route
            path="/meus-ingressos"
            element={
              <ProtectedRoute>
                <Navbar />
                <MyTickets />
              </ProtectedRoute>
            }
          />

          {/* Admin routes */}
          <Route
            path="/admin"
            element={
              <ProtectedRoute adminOnly>
                <AdminLayout />
              </ProtectedRoute>
            }
          >
            <Route index element={<Dashboard />} />
            <Route path="cinemas" element={<AdminTheaters />} />
            <Route path="salas" element={<AdminRooms />} />
            <Route path="cadeiras" element={<AdminChairs />} />
            <Route path="eventos" element={<AdminEvents />} />
            <Route path="ingressos" element={<AdminTickets />} />
            <Route path="sala-eventos" element={<AdminRoomEvents />} />
            <Route path="cadeiras-evento" element={<AdminChairsInEvent />} />
          </Route>

          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  )
}
