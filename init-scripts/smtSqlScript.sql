CREATE TABLE Medico (
  crmMed VARCHAR(8)   NOT NULL ,
  nomeMed VARCHAR(45)   NOT NULL ,
  sobrenomeMed VARCHAR(45)   NOT NULL ,
  telefoneMed VARCHAR(15)   NOT NULL ,
  emailMed VARCHAR(45)   NOT NULL ,
  senhaMed VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(crmMed));
INSERT INTO public.medico (crmmed, nomemed, sobrenomemed, telefonemed, emailmed, senhamed)
VALUES 
('1234567', 'Carlos', 'Silva', '(11) 98765-4321', 'carlos.silva@hospital.com', 'senha123'),
('2345678', 'Ana', 'Pereira', '(21) 99876-5432', 'ana.pereira@clinicamed.com', 'senha456'),
('3456789', 'Beatriz', 'Souza', '(31) 91234-5678', 'beatriz.souza@saudebem.com', 'senha789'),
('4567890', 'Roberto', 'Almeida', '(41) 92345-6789', 'roberto.almeida@saudeviva.com', 'seguro2024'),
('5678901', 'Mariana', 'Oliveira', '(51) 93456-7890', 'mariana.oliveira@hospitalprime.com', 'clinica01');
CREATE TABLE Especialidade (
  idEspecialidade SERIAL NOT NULL ,
  nomeEspec VARCHAR(35)   NOT NULL   ,
PRIMARY KEY(idEspecialidade));
INSERT INTO especialidade (nomeespec) 
VALUES 
  ('Cardiologia'),
  ('Neurologia'),
  ('Pediatria'),
  ('Ortopedia'),
  ('Dermatologia');
CREATE TABLE Unidade (
  idUnidade SERIAL NOT NULL ,
  nomeUnidade VARCHAR(20)   NOT NULL ,
  cepUni VARCHAR(10)   NOT NULL ,
  logradouroUni VARCHAR(45)   NOT NULL ,
  numeroUni VARCHAR(6)   NOT NULL ,
  bairroUni VARCHAR(20)   NOT NULL ,
  cidadeUni VARCHAR(20)   NOT NULL ,
  estadoUni VARCHAR(20)   NOT NULL ,
  paisUni VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(idUnidade));
INSERT INTO unidade (nomeunidade, cepuni, logradourouni, numerouni, bairrouni, cidadeuni, estadouni, paisuni)
VALUES
('Unidade A', '12345-678', 'Rua Exemplo A', '123', 'Bairro A', 'Cidade A', 'Estado A', 'Brasil'),
('Unidade B', '23456-789', 'Avenida Exemplo B', '456', 'Bairro B', 'Cidade B', 'Estado B', 'Brasil'),
('Unidade C', '34567-890', 'Rua Exemplo C', '789', 'Bairro C', 'Cidade C', 'Estado C', 'Brasil'),
('Unidade D', '45678-901', 'Avenida Exemplo D', '101', 'Bairro D', 'Cidade D', 'Estado D', 'Brasil'),
('Unidade E', '56789-012', 'Rua Exemplo E', '202', 'Bairro E', 'Cidade E', 'Estado E', 'Brasil');
CREATE TABLE Paciente (
  cpfPaci VARCHAR(14)   NOT NULL ,
  nomePaci VARCHAR(45)   NOT NULL ,
  sobrenomePaci VARCHAR(45)   NOT NULL ,
  nascimentoPaci DATE   NOT NULL ,
  generoPaci VARCHAR(15)   NOT NULL ,
  emailPaci VARCHAR(45)   NOT NULL ,
  telefonePaci VARCHAR(15)   NOT NULL ,
  senhaPaci VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(cpfPaci));
INSERT INTO Paciente (cpfPaci, nomePaci, sobrenomePaci, nascimentoPaci, generoPaci, emailPaci, telefonePaci, senhaPaci) VALUES
('123.456.789-00', 'Ana', 'Silva', '1990-05-15', 'Feminino', 'ana.silva@example.com', '(11) 91234-5678', 'senha123'),
('987.654.321-00', 'Bruno', 'Souza', '1985-08-20', 'Masculino', 'bruno.souza@example.com', '(21) 92345-6789', 'senha456'),
('111.222.333-44', 'Carla', 'Oliveira', '1992-12-30', 'Feminino', 'carla.oliveira@example.com', '(31) 93456-7890', 'senha789'),
('555.666.777-88', 'Daniel', 'Pereira', '1980-03-10', 'Masculino', 'daniel.pereira@example.com', '(41) 94567-8901', 'senha101'),
('333.444.555-99', 'Eliane', 'Costa', '1975-07-25', 'Feminino', 'eliane.costa@example.com', '(51) 95678-9012', 'senha202');
CREATE TABLE Atendente (
  ctpsAtend VARCHAR(15)   NOT NULL ,
  nomeAtend VARCHAR(45)   NOT NULL ,
  sobrenomeAtend VARCHAR(45)   NOT NULL ,
  inicioTurnoAtend TIME   NOT NULL ,
  fimTurnoAtend TIME   NOT NULL ,
  senhaAtend VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(ctpsAtend));
INSERT INTO Atendente (ctpsAtend, nomeAtend, sobrenomeAtend, inicioTurnoAtend, fimTurnoAtend, senhaAtend)
VALUES 
('0', 'SMT', 'Mobile', '00:00:00', '00:00:00', '00000'),
('987654321', 'Maria', 'Oliveira', '10:00:00', '18:00:00', 'senha456'),
('112233445', 'Carlos', 'Souza', '07:00:00', '15:00:00', 'senha789'),
('998877665', 'Fernanda', 'Costa', '09:00:00', '17:00:00', 'senha101112'),
('556677889', 'Ana', 'Pereira', '08:30:00', '16:30:00', 'senha131415');
CREATE TABLE Administrador (
  idAdministrador SERIAL,
  Unidade_idUnidade INTEGER   NOT NULL ,
  nomeAdm VARCHAR(20)    ,
  sobrenomeAdm VARCHAR(20)    ,
  senhaAdm VARCHAR(20)      ,
PRIMARY KEY(idAdministrador, Unidade_idUnidade)  ,
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));
INSERT INTO public.administrador (unidade_idunidade, nomeadm, sobrenomeadm, senhaadm)
VALUES 
(3, 'Carlos', 'Silva', 'senha123'),
(4, 'Mariana', 'Costa', 'adm456'),
(2, 'Pedro', 'Almeida', 'admin789'),
(5, 'Ana', 'Oliveira', 'seguro123'),
(1, 'Lucas', 'Santos', 'pass321');    
CREATE TABLE rel_med_unidade (
  Medico_crmMed VARCHAR(8)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Unidade_idUnidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));
INSERT INTO rel_med_unidade (Medico_crmMed, Unidade_idUnidade)
VALUES 
('1234567', 1),  -- Carlos Silva associado à Unidade A
('2345678', 2),  -- Ana Pereira associada à Unidade B
('3456789', 3),  -- Beatriz Souza associada à Unidade C
('4567890', 4),  -- Roberto Almeida associado à Unidade D
('5678901', 5);  -- Mariana Oliveira associada à Unidade E
CREATE TABLE rel_uni_espec (
  Unidade_idUnidade INTEGER   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL   ,
PRIMARY KEY(Unidade_idUnidade, Especialidade_idEspecialidade)    ,
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));
INSERT INTO rel_uni_espec (Unidade_idUnidade, Especialidade_idEspecialidade)
VALUES 
(1, 1),  -- Unidade A associada à Cardiologia
(2, 2),  -- Unidade B associada à Neurologia
(3, 3),  -- Unidade C associada à Pediatria
(4, 4),  -- Unidade D associada à Ortopedia
(5, 5);  -- Unidade E associada à Dermatologia
CREATE TABLE Disponibilidade (
  Medico_crmMed VARCHAR(8)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL ,
  dispoDom VARCHAR(3)   NOT NULL ,
  dispoSeg VARCHAR(3)   NOT NULL ,
  dispoTer VARCHAR(3)   NOT NULL ,
  dispoQua VARCHAR(3)   NOT NULL ,
  dispoQui VARCHAR(3)   NOT NULL ,
  dispoSex VARCHAR(3)   NOT NULL ,
  dispoSab VARCHAR(3)   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Unidade_idUnidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));
INSERT INTO Disponibilidade (Medico_crmMed, Unidade_idUnidade, dispoDom, dispoSeg, dispoTer, dispoQua, dispoQui, dispoSex, dispoSab)
VALUES 
('1234567', 1, 'Sim', 'Sim', 'Não', 'Sim', 'Sim', 'Não', 'Sim'),  -- Carlos Silva na Unidade A
('2345678', 2, 'Não', 'Sim', 'Sim', 'Sim', 'Não', 'Sim', 'Não'),  -- Ana Pereira na Unidade B
('3456789', 3, 'Sim', 'Não', 'Sim', 'Sim', 'Sim', 'Sim', 'Não'),  -- Beatriz Souza na Unidade C
('4567890', 4, 'Sim', 'Sim', 'Não', 'Não', 'Sim', 'Sim', 'Sim'),  -- Roberto Almeida na Unidade D
('5678901', 5, 'Não', 'Sim', 'Sim', 'Sim', 'Não', 'Não', 'Sim');  -- Mariana Oliveira na Unidade E
CREATE TABLE rel_med_espec (
  Medico_crmMed VARCHAR(8)   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Especialidade_idEspecialidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));
INSERT INTO rel_med_espec (Medico_crmMed, Especialidade_idEspecialidade)VALUES 
('1234567', 1),  -- Carlos Silva associado a Cardiologia
('2345678', 2),  -- Ana Pereira associada a Neurologia
('3456789', 3),  -- Beatriz Souza associada a Pediatria
('4567890', 4),  -- Roberto Almeida associado a Ortopedia
('5678901', 5);  -- Mariana Oliveira associada a Dermatologia
CREATE TABLE Consulta (
  idConsulta SERIAL,
  Paciente_cpfPaci VARCHAR(14)   NOT NULL ,
  Atendente_ctpsAtend VARCHAR(15)   NOT NULL ,
  Medico_crmMed VARCHAR(7)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL ,
  dataConsul DATE   NOT NULL ,
  horaConsul TIME   NOT NULL ,
  statusConsul VARCHAR(12)   NOT NULL   ,
PRIMARY KEY(idConsulta, Paciente_cpfPaci, Atendente_ctpsAtend, Medico_crmMed, Unidade_idUnidade, Especialidade_idEspecialidade)          ,
  FOREIGN KEY(Paciente_cpfPaci)
    REFERENCES Paciente(cpfPaci),
  FOREIGN KEY(Atendente_ctpsAtend)
    REFERENCES Atendente(ctpsAtend),
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));
INSERT INTO Consulta (Paciente_cpfPaci, Atendente_ctpsAtend, Medico_crmMed, Unidade_idUnidade, Especialidade_idEspecialidade, dataConsul, horaConsul, statusConsul) VALUES 
('123.456.789-00', '0', '1234567', 1, 1, '2024-12-05', '10:00:00', 'Cadastrada'),
('987.654.321-00', '987654321', '2345678', 2, 2, '2024-12-06', '14:30:00', 'Concluida'),
('111.222.333-44', '112233445', '3456789', 3, 3, '2024-12-07', '08:00:00', 'Cancelada'),
('555.666.777-88', '998877665', '4567890', 4, 4, '2024-12-08', '11:30:00', 'Cadastrada'),
('333.444.555-99', '556677889', '5678901', 5, 5, '2024-12-09', '15:00:00', 'Concluida');
-- Função para contar consultas e contribuintes
CREATE OR REPLACE FUNCTION contar_consultas_e_contribuintes()
RETURNS TABLE(
    total_consultas INT,
    consultas_concluidas INT,
    consultas_canceladas INT,
    total_contribuintes INT
) AS
$$
BEGIN
    -- Contagem total de consultas
    SELECT COUNT(*) INTO total_consultas
    FROM public.consulta;

    -- Contagem de consultas com status 'Concluída'
    SELECT COUNT(*) INTO consultas_concluidas
    FROM public.consulta
    WHERE statusconsul = 'Concluida';

    -- Contagem de consultas com status 'Cancelada'
    SELECT COUNT(*) INTO consultas_canceladas
    FROM public.consulta
    WHERE statusconsul = 'Cancelada';

    -- Contagem de médicos e atendentes com CTPS diferente de '0'
    SELECT 
        (SELECT COUNT(*) FROM public.atendente WHERE ctpsatend != '0') + 
        (SELECT COUNT(*) FROM public.medico WHERE crmmed != '0') 
    INTO total_contribuintes;

    -- Retorno dos resultados
    RETURN NEXT;
END;
$$ LANGUAGE plpgsql;
-- Criando a view consultadetalhada
CREATE OR REPLACE VIEW public.consultadetalhada AS
SELECT 
    c.idconsulta AS IdConsulta,
    c.dataconsul AS DataConsulta,
    c.horaconsul AS HoraConsulta,
    c.paciente_cpfpaci AS CpfPaciente,
    e.nomeespec AS Especialidade,
    c.statusconsul AS StatusConsulta,
    FLOOR(RANDOM() * 9 + 1) AS Consultorio 
FROM 
    public.consulta c
JOIN 
    public.especialidade e ON c.especialidade_idespecialidade = e.idespecialidade;
-- Criando a view contribuintes
CREATE OR REPLACE VIEW public.contribuintes AS
SELECT 
    split_part(m.nomeMed, ' ', 1) AS nome,  -- Pega apenas o primeiro nome do médico
    'Médico' AS ocupacao,
    m.crmMed AS identificador,
    FLOOR(1 + (RANDOM() * 9)) AS "Consultorio ou Guiche",
    e.nomeEspec AS especialidade
FROM 
    public.Medico m
LEFT JOIN 
    public.rel_med_espec rme ON m.crmMed = rme.Medico_crmMed
LEFT JOIN 
    public.Especialidade e ON rme.Especialidade_idEspecialidade = e.idEspecialidade
WHERE 
    m.crmMed != '0'
UNION
SELECT 
    split_part(a.nomeAtend, ' ', 1) AS nome,  -- Pega apenas o primeiro nome do atendente
    'Atendente' AS ocupacao,
    a.ctpsAtend AS identificador,
    FLOOR(1 + (RANDOM() * 9)) AS "Consultorio ou Guiche",
    'N/A' AS especialidade
FROM 
    public.Atendente a
WHERE 
    a.ctpsAtend != '0';
--
CREATE OR REPLACE VIEW public.consultapaciente
 AS
 SELECT c.idconsulta,
    c.dataconsul AS dataconsulta,
    c.horaconsul AS horaconsulta,
    p.nomepaci AS nomepaciente,
    p.nascimentopaci AS datanascimento,
    p.cpfpaci AS cpfpaciente,
    p.generopaci AS sexopaciente
   FROM consulta c
     JOIN paciente p ON c.paciente_cpfpaci = p.cpfpaci
  WHERE c.idconsulta = @c.idconsulta;
--
CREATE OR REPLACE VIEW public.medicosespecialidades
 AS
 SELECT m.crmmed AS crmmedico,
    m.nomemed AS nomemedico,
    m.sobrenomemed AS sobrenomemedico,
    e.nomeespec AS especialidadenome,
    rmu.unidade_idunidade AS unidadeid
   FROM medico m
     JOIN rel_med_unidade rmu ON m.crmmed = rmu.medico_crmmed
     JOIN rel_uni_espec rues ON rmu.unidade_idunidade = rues.unidade_idunidade
     JOIN especialidade e ON rues.especialidade_idespecialidade = e.idespecialidade;

SELECT * FROM public.consultadetalhada;
SELECT * FROM public.contribuintes;
SELECT * FROM public.consultapaciente;
SELECT * FROM public.medicosespecialidades;