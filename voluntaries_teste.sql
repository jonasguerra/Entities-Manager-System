PGDMP         
                w         
   manager_db     11.3 (Ubuntu 11.3-1.pgdg19.04+1)     11.3 (Ubuntu 11.3-1.pgdg19.04+1)     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �           1262    24585 
   manager_db    DATABASE     |   CREATE DATABASE manager_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'pt_BR.UTF-8' LC_CTYPE = 'pt_BR.UTF-8';
    DROP DATABASE manager_db;
             admin    false            �            1259    24918    address    TABLE     !  CREATE TABLE public.address (
    address_id character varying(36) NOT NULL,
    cep character varying(300),
    avenue character varying(300),
    number character varying(300),
    neighborhood character varying(300),
    city character varying(300),
    state character varying(300)
);
    DROP TABLE public.address;
       public         kaspary    false            �            1259    24908    user    TABLE       CREATE TABLE public."user" (
    user_id character varying(36) NOT NULL,
    email character varying(300) NOT NULL,
    password character varying(300) NOT NULL,
    is_approved boolean DEFAULT false NOT NULL,
    is_entity boolean DEFAULT false NOT NULL
);
    DROP TABLE public."user";
       public         kaspary    false            �            1259    24934 	   voluntary    TABLE     }  CREATE TABLE public.voluntary (
    voluntary_id character varying(36) NOT NULL,
    name character varying(300) NOT NULL,
    phone character varying(300) NOT NULL,
    affinity character varying(300),
    socialnetwork character varying(300),
    photoimagename character varying(300),
    user_id character varying(36) NOT NULL,
    address_id character varying(36) NOT NULL
);
    DROP TABLE public.voluntary;
       public         kaspary    false            �          0    24918    address 
   TABLE DATA               ]   COPY public.address (address_id, cep, avenue, number, neighborhood, city, state) FROM stdin;
    public       kaspary    false    197   �       �          0    24908    user 
   TABLE DATA               R   COPY public."user" (user_id, email, password, is_approved, is_entity) FROM stdin;
    public       kaspary    false    196   �       �          0    24934 	   voluntary 
   TABLE DATA               |   COPY public.voluntary (voluntary_id, name, phone, affinity, socialnetwork, photoimagename, user_id, address_id) FROM stdin;
    public       kaspary    false    198   �       	           2606    24925    address address_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pkey PRIMARY KEY (address_id);
 >   ALTER TABLE ONLY public.address DROP CONSTRAINT address_pkey;
       public         kaspary    false    197                       2606    24917    user user_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (user_id);
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT user_pkey;
       public         kaspary    false    196                       2606    24945 "   voluntary voluntary_address_id_key 
   CONSTRAINT     c   ALTER TABLE ONLY public.voluntary
    ADD CONSTRAINT voluntary_address_id_key UNIQUE (address_id);
 L   ALTER TABLE ONLY public.voluntary DROP CONSTRAINT voluntary_address_id_key;
       public         kaspary    false    198                       2606    24941    voluntary voluntary_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.voluntary
    ADD CONSTRAINT voluntary_pkey PRIMARY KEY (voluntary_id);
 B   ALTER TABLE ONLY public.voluntary DROP CONSTRAINT voluntary_pkey;
       public         kaspary    false    198                       2606    24943    voluntary voluntary_user_id_key 
   CONSTRAINT     ]   ALTER TABLE ONLY public.voluntary
    ADD CONSTRAINT voluntary_user_id_key UNIQUE (user_id);
 I   ALTER TABLE ONLY public.voluntary DROP CONSTRAINT voluntary_user_id_key;
       public         kaspary    false    198                       2606    24951 #   voluntary voluntary_address_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.voluntary
    ADD CONSTRAINT voluntary_address_id_fkey FOREIGN KEY (address_id) REFERENCES public.address(address_id);
 M   ALTER TABLE ONLY public.voluntary DROP CONSTRAINT voluntary_address_id_fkey;
       public       kaspary    false    197    198    2825                       2606    24946     voluntary voluntary_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.voluntary
    ADD CONSTRAINT voluntary_user_id_fkey FOREIGN KEY (user_id) REFERENCES public."user"(user_id);
 J   ALTER TABLE ONLY public.voluntary DROP CONSTRAINT voluntary_user_id_fkey;
       public       kaspary    false    2823    196    198            �   �   x����M�@�����4`4�x^��-{�A���"�P
��"R����(%���1
0y�J�5b*8�!���}�L��v�/��!!�ֿ�c~�<������lǼ~��m��sW����
�����Ф�t�I�< wE�|
JH �5��0W��A�!�&�,�VA3@wi�FY����=C��HϮ���؍�Y�rS{�F��ysm6 ��Ngb6��y����V��      �   �   x����m�0 ����"%��wg�G"E @RE�}:K�G��++5p�Kah)@8�s�k��x�|~��~ݏ����͏��~���l��^M�bsK:!A���.)�4I�:D�����>۬��k�jb�0���`���)��k)WVX]pv��Ng+���P�KJ6$*��W��Z4i5��}��}�+��d      �   �  x���1�T1D��O1!,Y��#p"ٲ
�O�ˁ8CN@���u�4�6R��8p� ��0�� m�ǧ���o���zݎw�/�R��;>��z�����������vŢc�S���K�� t�=L��1|0.qp� 6r0^:Sk��g�<
A�����`T�LtLhy@�d����X�*�c��e�M۔�i>}�o�)[�ї�1��bf�f�m��eM)<��&撾@FZf
%] ���5��Y�A�%pB���;(����F�x�cmMyI�9҈$�`bpQu�ͬ�I���b��ns�Lm�2W�%�c}�Qu�VaȖ��]��	T�)�a�~�Q���<k\�ad�@k��ح��ia�z��C��I�0����w{`�gI�rn4�.���'e���V]���}d����F2Qդ)3,4p��Ε��Hn�ݕ{EX�&�s}`�gQ�9Q�ik�j0Zk�&��$��gp�������99Õ����y�<�l     