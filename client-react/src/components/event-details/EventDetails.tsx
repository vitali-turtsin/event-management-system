import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, createSearchParams } from 'react-router-dom';
import EventService from '../../services/EventService';
import PersonService from '../../services/PersonService';
import OrganizationService from '../../services/OrganizationService';
import PaymentMethodService from '../../services/PaymentMethodService';
import { IListParticipant } from '../../models/IListParticipant';
import { IEvent } from '../../models/IEvent';
import { IPaymentMethod } from '../../models/IPaymentMethod';
import { IParticipant } from '../../models/IParticipant';
import { IOrganization } from '../../models/IOrganization';
import { IPerson } from '../../models/IPerson';
import TitleBanner from '../title-banner/TitleBanner';
import './EventDetails.css';

const EventDetails = () => {
    const { id: eventId } = useParams();
    const navigate = useNavigate();

    const [event, setEvent] = useState<IEvent | null>(null);
    const [eventParticipants, setEventParticipants] = useState<IListParticipant[]>([]);
    const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
    const [newParticipant, setNewParticipant] = useState<IParticipant>({
        isOrganization: false,
        firstName: "",
        lastName: "",
        personalCode: "",
        paymentMethodId: "",
        numberOfParticipants: 0,
        description: "",
    });

    useEffect(() => {
        EventService.get(eventId!).then((event) => {
            setEvent(event.data);
            setEventParticipants([
                ...(event.data.people?.map((person: IPerson): IListParticipant => ({
                    id: person.id,
                    name: person.firstName + " " + person.lastName,
                    code: person.personalCode,
                    updatedAt: person.updatedAt,
                    isOrganization: false,
                })) || []),
                ...(event.data.organizations?.map((organization: IOrganization): IListParticipant => ({
                    id: organization.id,
                    name: organization.name,
                    code: organization.registrationNumber,
                    updatedAt: organization.updatedAt,
                    isOrganization: true,
                })) || []),
            ]);
        });

        PaymentMethodService.getAll().then((response) => {
            const paymentMethods = response.data;
            setPaymentMethods(paymentMethods);
            if (paymentMethods[0].id) {
                setNewParticipant(prev => ({ ...prev, paymentMethodId: paymentMethods[0].id! }));
            }
        });
    }, [eventId]);

    const onSubmit = () => {
        if (!eventId) return;

        newParticipant.isOrganization
            ? createOrganization()
            : createPerson();
    };

    const removeParticipant = (id: string) => {
        if (!id) return;

        if (eventParticipants.find((p) => p.id === id)?.isOrganization) {
            OrganizationService.remove(id).then(() => {
                setEventParticipants(prev => prev.filter((p) => p.id !== id));
            });
            return;
        }

        PersonService.remove(id).then(() => {
            setEventParticipants(prev => prev.filter((p) => p.id !== id));
        });
    };

    const switchParticipantType = () => {
        setNewParticipant(prev => ({ ...prev, isOrganization: !prev.isOrganization }));
    };

    const createPerson = () => {
        const person: IPerson = {
            firstName: newParticipant.firstName,
            lastName: newParticipant.lastName,
            personalCode: newParticipant.personalCode,
            eventId: eventId!,
            paymentMethodId: newParticipant.paymentMethodId,
            description: newParticipant.description,
        };

        PersonService.post(person).then((response) => {
            if (response.data.id) {
                setEventParticipants(prev => [...prev, {
                    id: response.data.id,
                    name: person.firstName + " " + person.lastName,
                    code: person.personalCode,
                    updatedAt: person.updatedAt || new Date(),
                    isOrganization: false,
                }]);
            }
        });
    };

    const createOrganization = () => {
        const organization = {
            name: newParticipant.firstName + " " + newParticipant.lastName,
            description: newParticipant.description,
            registrationNumber: newParticipant.personalCode,
            numberOfParticipants: newParticipant.numberOfParticipants,
            eventId,
            paymentMethodId: newParticipant.paymentMethodId,
        } as IOrganization;

        OrganizationService.post(organization).then((response) => {
            if (response.data.id) {
                setEventParticipants(prev => [...prev, {
                    id: response.data.id,
                    name: organization.name,
                    code: organization.registrationNumber,
                    eventId: organization.eventId!,
                    updatedAt: organization.updatedAt || new Date(),
                    isOrganization: true,
                } as IListParticipant]);
            }
        });
    };

    return (
        <div className="form-page-container">
            <TitleBanner title="Participants" />

            <div className="form-container">
                <div className="title">Participants</div>

                <div className="form">
                    <div className="form-group">
                        <span className="label">Event Name:</span>
                        <span className="input">{event?.name}</span>
                    </div>
                    <div className="form-group">
                        <span className="label">Date:</span>
                        <span>{new Date(event?.dateTime ?? "").toLocaleDateString()}</span>
                    </div>
                    <div className="form-group">
                        <span className="label">Location:</span>
                        <span>{event?.location}</span>
                    </div>
                    <div className="form-group">
                        <div className="label"></div>
                        <table className="participants-table">
                            {eventParticipants.map((participant, i) => (
                                <tr key={participant.id}>
                                    <td>{i + 1}</td>
                                    <td>{participant.name}</td>
                                    <td>{participant.code}</td>
                                    <td>
                                        <a
                                            href={`/participants/${participant.id}`}
                                            onClick={(e) => {
                                                e.preventDefault();
                                                navigate({
                                                    pathname: `/participants/${participant.id}`,
                                                    search: createSearchParams({ isOrganization: participant.isOrganization.toString() }).toString(),
                                                });
                                            }}
                                        >
                                            VIEW
                                        </a>
                                    </td>
                                    <td
                                        className="remove-button"
                                        onClick={() => removeParticipant(participant.id!)}
                                    >
                                        REMOVE
                                    </td>
                                </tr>
                            ))}
                        </table>
                    </div>
                </div>

                <div className="title">Add Participants</div>

                <div className="form">
                    <div className="participant-radio">
                        <span>
                            <input
                                type="radio"
                                id="individual"
                                name="participantType"
                                checked={!newParticipant.isOrganization}
                                onChange={switchParticipantType}
                            />
                            <label htmlFor="individual">Person</label>
                        </span>
                        <span>
                            <input
                                type="radio"
                                id="company"
                                name="participantType"
                                checked={newParticipant.isOrganization}
                                onChange={switchParticipantType}
                            />
                            <label htmlFor="company">Organization</label>
                        </span>
                    </div>
                    <div className="form-group">
                        <label htmlFor="name">
                            {newParticipant.isOrganization ? "Name" : "First Name"}:
                        </label>
                        <input
                            type="text"
                            id="name"
                            value={newParticipant.firstName}
                            onChange={(e) =>
                                setNewParticipant((prev) => ({
                                    ...prev,
                                    firstName: e.target.value,
                                }))
                            }
                        />
                    </div>

                    {!newParticipant.isOrganization && (
                        <div className="form-group">
                            <label htmlFor="last-name">Last Name:</label>
                            <input
                                type="text"
                                id="last-name"
                                value={newParticipant.lastName}
                                onChange={(e) =>
                                    setNewParticipant((prev) => ({
                                        ...prev,
                                        lastName: e.target.value,
                                    }))
                                }
                            />
                        </div>
                    )}

                    <div className="form-group">
                        <label htmlFor="code">
                            {newParticipant.isOrganization ? "Reg nr" : "Personal Code"}:
                        </label>
                        <input
                            type="text"
                            id="code"
                            value={newParticipant.personalCode}
                            onChange={(e) =>
                                setNewParticipant((prev) => ({
                                    ...prev,
                                    personalCode: e.target.value,
                                }))
                            }
                        />
                    </div>

                    {newParticipant.isOrganization && (
                        <div className="form-group">
                            <label htmlFor="numberOfParticipants">Event participants:</label>
                            <input
                                type="number"
                                id="numberOfParticipants"
                                value={newParticipant.numberOfParticipants}
                                onChange={(e) =>
                                    setNewParticipant((prev) => ({
                                        ...prev,
                                        numberOfParticipants: parseInt(e.target.value),
                                    }))
                                }
                            />
                        </div>
                    )}

                    <div className="form-group">
                        <label htmlFor="paymentMethod">Payment by:</label>
                        <select
                            id="paymentMethod"
                            className="input"
                            value={newParticipant.paymentMethodId}
                            onChange={(e) =>
                                setNewParticipant((prev) => ({
                                    ...prev,
                                    paymentMethodId: e.target.value,
                                }))
                            }
                        >
                            {paymentMethods.map((paymentMethod) => (
                                <option key={paymentMethod.id} value={paymentMethod.id}>
                                    {paymentMethod.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="description">Description:</label>
                        <textarea
                            id="description"
                            className="input"
                            value={newParticipant.description}
                            onChange={(e) =>
                                setNewParticipant((prev) => ({
                                    ...prev,
                                    description: e.target.value,
                                }))
                            }
                        ></textarea>
                    </div>
                </div>

                <div className="buttons">
                    <button className="button secondary" onClick={() => navigate("/")}>
                        Back
                    </button>
                    <button className="button primary" onClick={onSubmit}>
                        Save
                    </button>
                </div>
            </div>
        </div>
    );
};

export default EventDetails;