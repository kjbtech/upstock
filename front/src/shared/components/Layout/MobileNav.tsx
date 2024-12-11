import React, { useState } from 'react';
import { Menu, X } from 'lucide-react';
import { navigation, NavItem } from './Navigation';
import { translations } from '@/shared/i18n/translations';

export function MobileNav() {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="sm:hidden">
      <button
        type="button"
        onClick={() => setIsOpen(!isOpen)}
        className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-gray-500 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
      >
        <span className="sr-only">{translations.navigation.openMenu}</span>
        {isOpen ? (
          <X className="block h-6 w-6" />
        ) : (
          <Menu className="block h-6 w-6" />
        )}
      </button>

      {isOpen && (
        <div className="absolute top-16 inset-x-0 bg-white border-b border-gray-200">
          <div className="pt-2 pb-3 space-y-1">
            {navigation.map((item) => (
              <NavItem
                key={item.name}
                item={item}
                isMobile
                onClick={() => setIsOpen(false)}
              />
            ))}
          </div>
        </div>
      )}
    </div>
  );
}