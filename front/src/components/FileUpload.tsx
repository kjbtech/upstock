import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import { Upload, File, X } from 'lucide-react';
import { clsx } from 'clsx';

interface FileUploadProps {
  onFilesSelected: (files: File[]) => void;
  acceptedFiles: File[];
  onRemoveFile: (file: File) => void;
}

export function FileUpload({ onFilesSelected, acceptedFiles, onRemoveFile }: FileUploadProps) {
  const onDrop = useCallback((droppedFiles: File[]) => {
    onFilesSelected(droppedFiles);
  }, [onFilesSelected]);

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: {
      'application/pdf': ['.pdf'],
      'image/*': ['.png', '.jpg', '.jpeg']
    }
  });

  return (
    <div className="w-full space-y-4">
      <div
        {...getRootProps()}
        className={clsx(
          "border-2 border-dashed rounded-lg p-8 text-center cursor-pointer transition-colors",
          isDragActive ? "border-blue-500 bg-blue-50" : "border-gray-300 hover:border-blue-400"
        )}
      >
        <input {...getInputProps()} />
        <Upload className="mx-auto h-12 w-12 text-gray-400" />
        <p className="mt-2 text-sm text-gray-600">
          Drag & drop PDF or image files here, or click to select files
        </p>
        <p className="text-xs text-gray-500 mt-1">
          Supported formats: PDF, PNG, JPG, JPEG
        </p>
      </div>

      {acceptedFiles.length > 0 && (
        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-700">Selected files:</h3>
          <ul className="divide-y divide-gray-200 border rounded-lg">
            {acceptedFiles.map((file, index) => (
              <li key={index} className="flex items-center justify-between p-3">
                <div className="flex items-center space-x-3">
                  <File className="h-5 w-5 text-gray-400" />
                  <span className="text-sm text-gray-700">{file.name}</span>
                  <span className="text-xs text-gray-500">
                    ({(file.size / 1024 / 1024).toFixed(2)} MB)
                  </span>
                </div>
                <button
                  onClick={() => onRemoveFile(file)}
                  className="text-red-500 hover:text-red-700"
                >
                  <X className="h-5 w-5" />
                </button>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}