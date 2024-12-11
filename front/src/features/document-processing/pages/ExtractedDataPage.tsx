import React from 'react';
import { FileText } from 'lucide-react';
import { ResultsSection } from '../components/ResultsSection';
import { useExtractedData } from '../hooks/useExtractedData';
import { translations } from '@/shared/i18n/translations';

export function ExtractedDataPage() {
  const { data, isLoading } = useExtractedData();

  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <div className="text-center mb-12">
        <FileText className="mx-auto h-12 w-12 text-blue-600" />
        <h1 className="mt-4 text-3xl font-bold text-gray-900">
          {translations.extractedData.title}
        </h1>
        <p className="mt-2 text-gray-600">
          {translations.extractedData.subtitle}
        </p>
      </div>

      <ResultsSection data={data} isLoading={isLoading} />
    </div>
  );
}