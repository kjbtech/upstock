import React from 'react';
import { MainNav } from './MainNav';
import { MobileNav } from './MobileNav';

interface LayoutProps {
  children: React.ReactNode;
}

export function Layout({ children }: LayoutProps) {
  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto">
          <div className="relative flex justify-between items-center h-16 px-4 sm:px-6 lg:px-8">
            <MainNav />
            <MobileNav />
          </div>
        </div>
      </header>
      <main>{children}</main>
    </div>
  );
}