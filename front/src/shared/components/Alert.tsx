import React from 'react';
import { AlertCircle, CheckCircle2, XCircle } from 'lucide-react';
import { clsx } from 'clsx';

type AlertType = 'success' | 'error';

interface AlertProps {
  type: AlertType;
  message: string;
  onClose?: () => void;
}

export function Alert({ type, message, onClose }: AlertProps) {
  const icons = {
    success: CheckCircle2,
    error: XCircle,
  };

  const Icon = icons[type];

  return (
    <div
      className={clsx(
        'rounded-lg p-4 flex items-center justify-between',
        type === 'success' && 'bg-green-50 text-green-800',
        type === 'error' && 'bg-red-50 text-red-800'
      )}
    >
      <div className="flex items-center space-x-3">
        <Icon className="h-5 w-5" />
        <span className="text-sm font-medium">{message}</span>
      </div>
      {onClose && (
        <button
          onClick={onClose}
          className="text-sm font-medium hover:opacity-75"
        >
          Dismiss
        </button>
      )}
    </div>
  );
}