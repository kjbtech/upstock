import React from 'react';
import { FileUpload } from '@/shared/components/FileUpload';
import { ProgressBar } from '@/shared/components/ProgressBar';
import { translations } from '@/shared/i18n/translations';

interface UploadSectionProps {
  files: File[];
  isUploading: boolean;
  uploadProgress: number;
  onFilesSelected: (files: File[]) => void;
  onRemoveFile: (file: File) => void;
  onUpload: () => Promise<void>;
}

export function UploadSection({
  files,
  isUploading,
  uploadProgress,
  onFilesSelected,
  onRemoveFile,
  onUpload,
}: UploadSectionProps) {
  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-lg font-medium text-gray-900 mb-4">{translations.upload.title}</h2>
      <FileUpload
        onFilesSelected={onFilesSelected}
        acceptedFiles={files}
        onRemoveFile={onRemoveFile}
      />
      {files.length > 0 && (
        <div className="mt-4 space-y-4">
          {isUploading && (
            <div className="space-y-2">
              <ProgressBar progress={uploadProgress} />
              <p className="text-sm text-gray-500 text-center">
                {translations.upload.uploading} {uploadProgress}%
              </p>
            </div>
          )}
          <button
            onClick={onUpload}
            disabled={isUploading}
            className={`w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white ${
              isUploading
                ? 'bg-blue-400 cursor-not-allowed'
                : 'bg-blue-600 hover:bg-blue-700'
            }`}
          >
            {isUploading ? translations.upload.uploading : translations.upload.processFiles}
          </button>
        </div>
      )}
    </div>
  );
}