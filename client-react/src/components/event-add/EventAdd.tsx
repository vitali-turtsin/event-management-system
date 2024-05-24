import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import TitleBanner from '../title-banner/TitleBanner';
import EventService from '../../services/EventService';

const EventAdd = () => {
    const [event, setEvent] = useState({
        name: "",
        dateTime: new Date().toISOString(),
        location: "",
        description: "",
    });

    const navigate = useNavigate();

    const onSubmit = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        if (event.dateTime) setEvent({ ...event, dateTime: new Date(event.dateTime).toISOString() });

        EventService.post(event).then(() => {
            navigate('/');
        });
    };

    return (
        <div className="form-page-container">
            <TitleBanner title="Add Event" />
            <div className="form-container">
                <div className="title">Add Event</div>
                <div className="form">
                    <div className="form-group">
                        <label htmlFor="name">Event Name:</label>
                        <input type="text" id="name" value={event.name} onChange={e => setEvent({ ...event, name: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="date">Time & date:</label>
                        <input type="datetime-local" id="date" value={event.dateTime} onChange={e => setEvent({ ...event, dateTime: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="location">Location:</label>
                        <input type="text" id="location" value={event.location} onChange={e => setEvent({ ...event, location: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="description">Description:</label>
                        <textarea className="input" id="description" value={event.description} onChange={e => setEvent({ ...event, description: e.target.value })}></textarea>
                    </div>
                </div>
                <div className="buttons">
                    <button className="button secondary" onClick={() => navigate('/')}>Back</button>
                    <button className="button primary" onClick={(e) => onSubmit(e)}>Add</button>
                </div>
            </div>
        </div>
    );
};

export default EventAdd;