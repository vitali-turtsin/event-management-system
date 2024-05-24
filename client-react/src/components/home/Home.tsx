import React, { useState, useEffect } from 'react';
import EventService from '../../services/EventService';
import './Home.css';
import { Link } from 'react-router-dom';
import { IEvent } from '../../models/IEvent';
import bannerImage from '../../assets/pilt.jpg';
import removeIcon from '../../assets/remove.svg';

const Home = () => {
    const [futureEvents, setFutureEvents] = useState<IEvent[]>([]);
    const [pastEvents, setPastEvents] = useState<IEvent[]>([]);

    const homeHtmlText =
        "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium <b>doloremque laudantium</b>, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non";

    useEffect(() => {
        loadEvents();
    }, []);

    const loadEvents = () => {
        const search = {
            sortField: "updatedAt",
        };

        EventService.getAll(search).then(response => {
            const events = response.data;

            setFutureEvents(events.filter(e => new Date(e.dateTime) > new Date()));
            setPastEvents(events.filter(e => new Date(e.dateTime) < new Date()));
        });
    };

    const removeEvent = (id: string) => {
        if (!id) return;

        EventService.remove(id).then(() => {
            loadEvents();
        });
    };

    return (
        <div className="home-container">
            <div className="home-banner">
                <div className="banner-text">
                    <span dangerouslySetInnerHTML={{ __html: homeHtmlText }}></span>
                </div>
                <img className="banner-image" src={bannerImage} alt="banner" />
            </div>
            <div className="event-lists">
                <div className="event-list">
                    <div className="event-list-header">Future Events</div>
                    <div className="event-list-body">
                        {!futureEvents.length && <div>No events</div>}
                        <table>
                            <tbody>
                                {futureEvents.map((event, i) => (
                                    <tr key={i}>
                                        <td>{i + 1}</td>
                                        <td>{event.name}</td>
                                        <td>{new Date(event.dateTime).toLocaleDateString()}</td>
                                        <td>
                                            <Link to={`/events/${event.id}`}>PARTICIPANTS</Link>
                                        </td>
                                        <td>
                                            <img
                                                className="remove-button"
                                                src={removeIcon}
                                                alt="remove"
                                                onClick={() => removeEvent(event.id!)}
                                            />
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <Link className="add-event" to="/add-event">ADD EVENT</Link>
                    </div>
                </div>

                <div className="event-list">
                    <div className="event-list-header">Past Events</div>
                    <div className="event-list-body">
                        {!pastEvents.length && <div>No events</div>}
                        <table>
                            <tbody>
                                {pastEvents.map((event, i) => (
                                    <tr key={i}>
                                        <td>{i + 1}</td>
                                        <td>{event.name}</td>
                                        <td>{new Date(event.dateTime).toLocaleDateString()}</td>
                                        <td>PARTICIPANTS</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Home;