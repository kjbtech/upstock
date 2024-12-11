import { useState, useEffect } from 'react';
import { ExtractedData } from '../domain/ExtractedData';
import { DocumentApiRepository } from '../infrastructure/DocumentApiRepository';
import { useAlert } from '@/shared/hooks/useAlert';

export function useExtractedData() {
  const [data, setData] = useState<ExtractedData[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { showAlert } = useAlert();

  const documentRepository = new DocumentApiRepository();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await documentRepository.getExtractedData([]);
        if (result.isFailure) {
          throw new Error(result.error || 'Failed to fetch data');
        }
        setData(result.getValue().items);
      } catch (error) {
        showAlert('error', error instanceof Error ? error.message : 'Failed to load data');
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [showAlert]);

  return { data, isLoading };
}