import React from 'react';
import { FileText } from 'lucide-react';
import { navigation, NavItem } from './Navigation';
import { translations } from '@/shared/i18n/translations';

export function MainNav() {
  return (
    <div className="flex">
      <div className="flex-shrink-0 flex items-center">
        <FileText className="h-8 w-8 text-blue-600" />
        <span className="ml-2 text-xl font-bold text-gray-900">
          {translations.appName}
        </span>
      </div>
      <div className="hidden sm:ml-6 sm:flex sm:space-x-8">
        {navigation.map((item) => (
          <NavItem key={item.name} item={item} />
        ))}
      </div>
    </div>
  );
}