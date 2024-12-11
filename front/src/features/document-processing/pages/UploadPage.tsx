import React from 'react';
import { Upload } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { Alert } from '@/shared/components/Alert';
import { UploadSection } from '../components/UploadSection';
import { useDocumentUpload } from '../hooks/useDocumentUpload';
import { useAlert } from '@/shared/hooks/useAlert';
import { translations } from '@/shared/i18n/translations';

export function UploadPage() {
  const navigate = useNavigate();
  const {
    files,
    isUploading,
    uploadProgress,
    handleFilesSelected,
    handleRemoveFile,
    handleUpload: handleUploadBase,
  } = useDocumentUpload();

  const { alert, clearAlert } = useAlert();

  const handleUpload = async () => {
    const result = await handleUploadBase();
    if (result.success) {
      navigate('/extracted-data');
    }
  };

  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <div className="text-center mb-12">
        <Upload className="mx-auto h-12 w-12 text-blue-600" />
        <h1 className="mt-4 text-3xl font-bold text-gray-900">
          {translations.upload.title}
        </h1>
        <p className="mt-2 text-gray-600">
          {translations.upload.subtitle}
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
      </div>
    </div>
  );
}