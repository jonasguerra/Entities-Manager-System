CREATE TABLE public.user
(
    user_id character varying(36) NOT NULL PRIMARY KEY,
    email character varying(300) NOT NULL,
    password character varying(300) NOT NULL,
    is_approved BOOLEAN NOT NULL DEFAULT FALSE,
    is_entity BOOLEAN NOT NULL DEFAULT FALSE,
    is_voluntary BOOLEAN NOT NULL DEFAULT FALSE,
    is_moderator BOOLEAN NOT NULL DEFAULT FALSE
)


CREATE TABLE public.address
(
    address_id character varying(36) NOT NULL PRIMARY KEY,
    cep character varying(300),
    avenue character varying(300),
    number character varying(300),
    neighborhood character varying(300),
    city character varying(300),
    state character varying(300)
)


CREATE TABLE public.affinity
(
    affinity_id character varying(36) NOT NULL PRIMARY KEY,
    name character varying(300) NOT NULL  
)

INSERT INTO affinity (affinity_id, name) values ('1ae4f4ed-5eee-4b7e-9242-3712433afc4e', 'Infantil');
INSERT INTO affinity (affinity_id, name) values ('44fce376-d295-4374-9872-3343ba3b4a85', 'Pets');
INSERT INTO affinity (affinity_id, name) values ('67a8874e-f74a-443e-893c-d790e5264ce6', 'Idosos');
INSERT INTO affinity (affinity_id, name) values ('b51e61ca-6f88-4308-b556-b925d5e5e181', 'Necessitados');
INSERT INTO affinity (affinity_id, name) values ('23296243-ec9d-4d7b-98f7-6cbf7cea169b', 'Meio Ambiente');


-- ######################### VOLUNTARY #########################

CREATE TABLE public.voluntary
(
    voluntary_id character varying(36) NOT NULL PRIMARY KEY,
    name character varying(300) NOT NULL,
    phone character varying(300) NOT NULL,
    socialnetwork character varying(300),
    
	user_id character varying(36) NOT NULL UNIQUE,
	address_id character varying(36) NOT NULL UNIQUE,
	
    FOREIGN KEY ("user_id") REFERENCES "user"("user_id"),
    FOREIGN KEY ("address_id") REFERENCES "address"("address_id")
)

CREATE TABLE public.voluntary_affinity
(
    affinity_id character varying(36) NOT NULL,
	voluntary_id character varying(36) NOT NULL,
	PRIMARY KEY(affinity_id, voluntary_id),
    FOREIGN KEY ("affinity_id") REFERENCES "affinity"("affinity_id"),
    FOREIGN KEY ("voluntary_id") REFERENCES "voluntary"("voluntary_id")
)

-- ######################### EVENT #########################

CREATE TABLE public.event
(
    event_id character varying(36) NOT NULL PRIMARY KEY,
    title character varying(300) NOT NULL,
    Description character varying(1000) NOT NULL,
    Date TIMESTAMP NOT NULL,
    
	address_id character varying(36) NOT NULL UNIQUE,
	
    FOREIGN KEY ("address_id") REFERENCES "address"("address_id")
)


CREATE TABLE public.event_affinity
(
    affinity_id character varying(36) NOT NULL,
	event_id character varying(36) NOT NULL,
	PRIMARY KEY(affinity_id, event_id),
    FOREIGN KEY ("affinity_id") REFERENCES "affinity"("affinity_id"),
    FOREIGN KEY ("event_id") REFERENCES "event"("event_id")
)

CREATE TABLE public.event_voluntary
(
    voluntary_id character varying(36) NOT NULL,
	event_id character varying(36) NOT NULL,
	PRIMARY KEY(voluntary_id, event_id),
    FOREIGN KEY ("voluntary_id") REFERENCES "voluntary"("voluntary_id"),
    FOREIGN KEY ("event_id") REFERENCES "event"("event_id")
)