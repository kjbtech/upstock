import { useState, useCallback } from 'react';

export type AlertType = 'success' | 'error';

interface Alert {
  type: AlertType;
  message: string;
}

export function useAlert() {
  const [alert, setAlert] = useState<Alert | null>(null);

  const showAlert = useCallback((type: AlertType, message: string) => {
    setAlert({ type, message });
  }, []);

  const clearAlert = useCallback(() => {
    setAlert(null);
  }, []);

  return {
    alert,
    showAlert,
    clearAlert,
  };
}