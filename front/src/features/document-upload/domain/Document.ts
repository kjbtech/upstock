export interface Document {
  id: string;
  name: string;
  size: number;
  type: string;
  status: DocumentStatus;
  uploadedAt: Date;
}

export type DocumentStatus = 'pending' | 'processing' | 'completed' | 'failed';