CREATE TABLE public.user
(
    user_id character varying(36) NOT NULL PRIMARY KEY,
    email character varying(300) NOT NULL,
    password character varying(300) NOT NULL,
    is_approved BOOLEAN NOT NULL DEFAULT FALSE,
    is_entity BOOLEAN NOT NULL DEFAULT FALSE
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


CREATE TABLE public.voluntary
(
    voluntary_id character varying(36) NOT NULL PRIMARY KEY,
    name character varying(300) NOT NULL,
    phone character varying(300) NOT NULL,
    socialnetwork character varying(300),
    photoimagename character varying(300),
    
	user_id character varying(36) NOT NULL UNIQUE,
	address_id character varying(36) NOT NULL UNIQUE,
	
    FOREIGN KEY ("user_id") REFERENCES "user"("user_id"),
    FOREIGN KEY ("address_id") REFERENCES "address"("address_id")
)


CREATE TABLE public.affinity
(
    affinity_id character varying(36) NOT NULL PRIMARY KEY,
    name character varying(300) NOT NULL  
)


CREATE TABLE public.voluntary_affinity
(
    affinity_id character varying(36) NOT NULL,
	voluntary_id character varying(36) NOT NULL,
	PRIMARY KEY(affinity_id, voluntary_id),
    FOREIGN KEY ("affinity_id") REFERENCES "affinity"("affinity_id"),
    FOREIGN KEY ("voluntary_id") REFERENCES "voluntary"("voluntary_id")
)