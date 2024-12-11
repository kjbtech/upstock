import React from 'react';
import { Upload } from 'lucide-react';
import { Alert } from '@/shared/components/Alert';
import { UploadSection } from './components/UploadSection';
import { ResultsSection } from './components/ResultsSection';
import { useDocumentUpload } from './hooks/useDocumentUpload';
import { useAlert } from '@/shared/hooks/useAlert';

export function DocumentProcessingFeature() {
  const {
    files,
    isUploading,
    uploadProgress,
    extractedData,
    handleFilesSelected,
    handleRemoveFile,
    handleUpload,
  } = useDocumentUpload();

  const { alert, clearAlert } = useAlert();

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <Upload className="mx-auto h-12 w-12 text-blue-600" />
          <h1 className="mt-4 text-3xl font-bold text-gray-900">
          UpStock
          </h1>
          <p className="mt-2 text-gray-600">
            Upload PDF documents and images to extract and analyze data
          </p>
        </div>

        {alert && (
          <div className="mb-6">
            <Alert
              type={alert.type}
              message={alert.message}
              onClose={clearAlert}
            />
          </div>
        )}

        <div className="space-y-8">
          <UploadSection
            files={files}
            isUploading={isUploading}
            uploadProgress={uploadProgress}
            onFilesSelected={handleFilesSelected}
            onRemoveFile={handleRemoveFile}
            onUpload={handleUpload}
          />
          <ResultsSection
            data={extractedData}
            isLoading={isUploading}
          />
        </div>
      </div>
    </div>
  );
}