import React from 'react';
import { Link } from 'react-router-dom';
import './Footer.css';

const infoByColumn = [
    {
        htmlColumnLabel: 'Curabitur',
        items: [
            { htmlLabel: 'Emauris', linkPath: '/emauris' },
            { htmlLabel: 'Khringilla', linkPath: '/khringilla' },
            { htmlLabel: 'Oin magna sem', linkPath: '/oin-magna-sem' },
            { htmlLabel: 'Sed consequat', linkPath: '/sed-consequat' },
        ],
    },
    {
        htmlColumnLabel: 'Fusce',
        items: [
            { htmlLabel: 'Emauris', linkPath: '/emauris' },
            { htmlLabel: 'Khringilla', linkPath: '/khringilla' },
            { htmlLabel: 'Oin magna sem', linkPath: '/oin-magna-sem' },
            { htmlLabel: 'Sed consequat', linkPath: '/sed-consequat' },
        ],
    },
    {
        htmlColumnLabel: 'Contact',
        items: [
            { htmlLabel: '<b>Office: Tallinas</b>' },
            { htmlLabel: 'Väike-Aasia 1, 22415 Tallinn' },
            { htmlLabel: 'Telefon: 691 1234' },
            { htmlLabel: 'Faks: 605 5678' },
        ],
    },
    {
        htmlColumnLabel: '&nbsp;',
        items: [
            { htmlLabel: '<b>Other office</b>' },
            { htmlLabel: 'Oja tn 7' },
            { htmlLabel: 'Telefon: 60512345' },
            { htmlLabel: 'FaksÖ 6050987' },
        ],
    },
] as { htmlColumnLabel?: string; items: { htmlLabel: string; linkPath?: string }[] }[];

const Footer = () => {
    return (
        <div className="footer">
            {infoByColumn.map((column, index) => (
                <div key={index} className="column">
                    <div className="column-title" dangerouslySetInnerHTML={{ __html: column.htmlColumnLabel || '' }} />
                    {column.items.map((item, i) => (
                        item.linkPath ?
                            <a key={i} href={item.linkPath} className="column-link" dangerouslySetInnerHTML={{ __html: item.htmlLabel }} /> :
                            <div key={i} dangerouslySetInnerHTML={{ __html: item.htmlLabel }} />
                    ))}
                </div>
            ))}
        </div>
    );
};

export default Footer;