import React from 'react';
import { NavLink } from 'react-router-dom';
import { Upload, FileText } from 'lucide-react';
import { clsx } from 'clsx';
import { translations } from '@/shared/i18n/translations';

export const navigation = [
  { name: translations.navigation.upload, to: '/', icon: Upload },
  { name: translations.navigation.extractedData, to: '/extracted-data', icon: FileText },
];

interface NavItemProps {
  item: typeof navigation[0];
  isMobile?: boolean;
  onClick?: () => void;
}

export function NavItem({ item, isMobile, onClick }: NavItemProps) {
  return (
    <NavLink
      key={item.name}
      to={item.to}
      onClick={onClick}
      className={({ isActive }) =>
        clsx(
          isMobile
            ? 'block pl-3 pr-4 py-2 border-l-4 text-base font-medium'
            : 'inline-flex items-center px-1 pt-1 text-sm font-medium',
          isActive
            ? isMobile
              ? 'bg-blue-50 border-blue-500 text-blue-700'
              : 'border-b-2 border-blue-500 text-gray-900'
            : isMobile
            ? 'border-transparent text-gray-500 hover:bg-gray-50 hover:border-gray-300 hover:text-gray-700'
            : 'border-b-2 border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700'
        )
      }
    >
      <div className="flex items-center">
        <item.icon className="h-4 w-4 mr-2" />
        {item.name}
      </div>
    </NavLink>
  );
}