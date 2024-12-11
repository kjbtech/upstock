import { Document } from './Document';
import { ExtractedDataCollection } from './ExtractedData';
import { Result } from '@/shared/core/Result';

export interface DocumentRepository {
  uploadDocuments(files: File[]): Promise<Result<Document[]>>;
  getExtractedData(documentIds: string[]): Promise<Result<ExtractedDataCollection>>;
}