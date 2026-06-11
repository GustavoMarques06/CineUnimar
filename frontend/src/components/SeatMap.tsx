import { ChairStatus, type ChairInEvent } from '../types'
import { val, numVal } from '../api/client'

interface Props {
  seats: ChairInEvent[]
  selectedId: string | null
  onSelect: (id: string) => void
}

export default function SeatMap({ seats, selectedId, onSelect }: Props) {
  if (seats.length === 0) {
    return (
      <div className="text-center py-10 text-gray-400">
        Nenhum assento cadastrado para este evento.
      </div>
    )
  }

  return (
    <div className="space-y-4">
      <div className="flex items-center gap-6 text-xs text-gray-500 justify-center">
        <span className="flex items-center gap-1.5">
          <span className="w-4 h-4 rounded bg-gray-100 border border-gray-300 inline-block" />
          Disponível
        </span>
        <span className="flex items-center gap-1.5">
          <span className="w-4 h-4 rounded bg-indigo-600 inline-block" />
          Selecionado
        </span>
        <span className="flex items-center gap-1.5">
          <span className="w-4 h-4 rounded bg-gray-400 inline-block" />
          Ocupado
        </span>
      </div>

      <div className="w-full bg-gray-200 rounded h-2 text-center text-[10px] text-gray-400 flex items-center justify-center mb-6">
        <span>TELA</span>
      </div>

      <div className="flex flex-wrap gap-2 justify-center max-w-xl mx-auto">
        {seats.map((seat, idx) => {
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          const id = val((seat as any).id) || (seat as any).id
          const status = numVal((seat as any).status)
          const isOccupied = status === ChairStatus.Occupied
          const isSelected = selectedId === id

          return (
            <button
              key={id || idx}
              disabled={isOccupied}
              onClick={() => onSelect(id)}
              title={`Assento ${idx + 1}`}
              className={`
                w-10 h-10 rounded text-xs font-semibold transition-all
                ${isOccupied
                  ? 'bg-gray-400 text-white cursor-not-allowed'
                  : isSelected
                  ? 'bg-indigo-600 text-white shadow-md scale-110'
                  : 'bg-gray-100 border border-gray-300 text-gray-700 hover:bg-indigo-100 hover:border-indigo-400'
                }
              `}
            >
              {idx + 1}
            </button>
          )
        })}
      </div>
    </div>
  )
}
