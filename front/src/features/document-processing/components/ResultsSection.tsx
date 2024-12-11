import React from 'react';
import { DataTable } from '@/shared/components/DataTable';
import { ExtractedData } from '../domain/ExtractedData';
import { translations } from '@/shared/i18n/translations';

interface ResultsSectionProps {
  data: ExtractedData[];
  isLoading: boolean;
}

export function ResultsSection({ data, isLoading }: ResultsSectionProps) {
  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-lg font-medium text-gray-900 mb-4">
        {translations.extractedData.title}
      </h2>
      <DataTable data={data} isLoading={isLoading} />
    </div>
  );
}