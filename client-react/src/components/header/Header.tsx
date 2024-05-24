import React, { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Header.css';
import logo from '../../assets/logo.svg';
import symbol from '../../assets/symbol.svg';

const links = [
    { label: 'Home', path: '/' },
    { label: 'Add Event', path: '/add-event' },
];

const Header = () => {
    const location = useLocation();
    const [selectedPath, setSelectedPath] = useState(location.pathname);

    useEffect(() => {
        setSelectedPath(location.pathname);
    }, [location]);

    return (
        <div className="header">
            <a href="/" className="header-title">
                <img src={logo} alt="Logo" />
            </a>
            <div className="navigation">
                <div className="links">
                    {links.map((link, index) => (
                        <Link
                            key={index}
                            to={link.path}
                            className={`link ${selectedPath === link.path ? 'selected' : ''}`}
                        >
                            {link.label}
                        </Link>
                    ))}
                </div>
                <div className="symbol">
                    <img src={symbol} alt="Symbol" />
                </div>
            </div>
        </div>
    );
};

export default Header;