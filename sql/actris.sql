


CREATE SCHEMA ACTRIS;

CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.REF_ActionTTrackingUser (
	EmpAccount varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Role] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpEmail varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyCode varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PosId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PosTitle varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentPosId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentPosTitle varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SectionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SectionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);



CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.REF_ActionTrackingPendingTask (
	TransactionNo varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Approver varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ApproverType varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WorkflowCode varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WorkflowDesc varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);



CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.REF_ActionTrackingUserRole (
	Username varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	RoleGroupName varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PosID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpEmail varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);



CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionPriority (
	CorrectiveActionPriorityID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PriorityTitle varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PriorityValue varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	PriorityTitle2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PriorityValue2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PriorityDuration int NULL,
	CONSTRAINT PK_MD_CorrectiveActionPriority PRIMARY KEY (CorrectiveActionPriorityID)
);


CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_ActionTrackingSource (
	ActionTrackingSourceID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	SourceTitle varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SourceValue varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'SYSTEM' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	SourceTitle2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SourceValue2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateRegionalID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateRegionalDesc varchar(550) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisiZonaID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisiZonaDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WilayahkerjaID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WilayahkerjaDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FunctionID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FunctionDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,	
	CONSTRAINT PK_MD_ActionTrackingSource PRIMARY KEY (ActionTrackingSourceID)
);


CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionImpactAnalysis (
CorrectiveActionImpactAnalysisID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AnalysisTitle varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AnalysisValue varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'SYSTEM' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	AnalysinTitle2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AnalysisValue2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_MD_CorrectiveActionImpactSnalysis PRIMARY KEY (CorrectiveActionImpactAnalysisID)
);




CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionOverdueImpact (
	CorrectiveActionOverdueImpactID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ImpactTitle varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ImpactValue varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'SYSTEM' NULL,
	CreatedDate datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	ImpactTitle2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ImpactValue2 varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_MD_CorrectiveActionOverdueImpact PRIMARY KEY (CorrectiveActionOverdueImpactID)
);






CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionFlowStatus (
	CorrectiveActionFlowStatusID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CorrectiveActionFlowStatusPendingID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FlowCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FlowDesc varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FlowRole varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FlowRoleCode varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'SYSTEM' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	ModifiedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'SYSTEM' NULL,
	ModifiedAt datetime DEFAULT getdate() NULL,
	CONSTRAINT PK_MD_CorrectiveActionFlowStatus PRIMARY KEY (CorrectiveActionFlowStatusID)
);



CREATE NONCLUSTERED INDEX IDX_MD_CorrectiveActionFlowStatus_FK ON ACTRIS.MD_CorrectiveActionFlowStatus ( CorrectiveActionFlowStatusPendingID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;



ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionFlowStatus ADD CONSTRAINT FK_TMD_CorrectiveActionFlowStatus_Pending FOREIGN KEY (CorrectiveActionFlowStatusPendingID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionFlowStatus(CorrectiveActionFlowStatusID);








CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.MD_FlowStatusNext (
	FlowStatusID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	FlowStatusNextID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_MD_FlowNext PRIMARY KEY (FlowStatusID,FlowStatusNextID),
	CONSTRAINT FK_MD_FlowStatusNext FOREIGN KEY (FlowStatusID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionFlowStatus(CorrectiveActionFlowStatusID),
	CONSTRAINT FK_MD_FlowStatusNext_Next FOREIGN KEY (FlowStatusNextID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionFlowStatus(CorrectiveActionFlowStatusID)
);

 CREATE NONCLUSTERED INDEX IDX_MD_FlowStatusNext_Next_FK ON ACTRIS.MD_FlowStatusNext (  FlowStatusNextID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

 CREATE NONCLUSTERED INDEX IDX_MD_FlowStatusNext_FK ON ACTRIS.MD_FlowStatusNext (  FlowStatusID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;




CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_ActionTracking (
	ActionTrackingID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LocationStatusID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActionTrackingSourceID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ObservationID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ActionTrackingReference varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IssueDate date NULL,
	FindingDesc varchar(4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LocationID varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Location varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubLocation varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Nct varchar(4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Rca varchar(4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Status varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedAt datetime NULL,
	AttachementData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifiedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifiedAt datetime NULL,
	AdditionalData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TrypeShuRegion varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateRegionalID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateRegionalDesc varchar(550) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisiZonaID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisiZonaDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyCode varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyName varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WilayahkerjaID varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	WilayahkerjaDesc varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsConfidential bit NULL,
	CONSTRAINT PK_TX_ActionTracking PRIMARY KEY (ActionTrackingID)
);


 CREATE NONCLUSTERED INDEX IDX_TX_ActionTracking_ActionTrackingSource_FK ON ACTRIS.TX_ActionTracking (  ActionTrackingSourceID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 
 CREATE NONCLUSTERED INDEX IDX_TX_ActionTracking_LocationStatus_FK ON ACTRIS.TX_ActionTracking (  LocationStatusID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 
 CREATE NONCLUSTERED INDEX IDX_TX_ActionTracking__Observation_FK ON ACTRIS.TX_ActionTracking (  ObservationID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;







CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionHistory (
	CorrectiveActionHistoryID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CorrectiveActionID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Action varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT  '--' NULL,
	ActionBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	ActionAt datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	NextAction varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	NextActionBy varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Note nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_TX_CorrectiveActionHistory PRIMARY KEY (CorrectiveActionHistoryID)
);





 CREATE NONCLUSTERED INDEX IDX_TX_CorrectiveActionHistory_CorrectiveAction_FK ON ACTRIS.TX_CorrectiveActionHistory (  CorrectiveActionID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;



CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction (
	CorrectiveActionID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CorrectiveActionOverdueImpactID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ActionTrackingID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CorrectiveActionPriorityID varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CorrectiveActionReference varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Draft' NULL,
	ResponsibleDivision varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleDivisionID varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleDepartment varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleDepartmentID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleManager varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleManagerApprover varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleManagerUsername varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponsibleDepartmentData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Recomendation nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DueDate date NULL,
	ProposedDueDate date NULL,
	ProposedDueDateApprover varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProposedDueDateData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompletionDate date NULL,
	PicData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AttachmentData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT '{}' NULL,
	FollowUpData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OverdueData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	AdditionalData nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	ModifiedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	ModifiedAt datetime DEFAULT getdate() NULL,
	Status varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	FlowCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	TransID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_TX_CorrectiveAction PRIMARY KEY (CorrectiveActionID)
);


 CREATE NONCLUSTERED INDEX IDX_TX_CorrectiveAction_ActionTracking_FK ON ACTRIS.TX_CorrectiveAction (  ActionTrackingID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 
 CREATE NONCLUSTERED INDEX IDX_TX_CorrectiveAction_OverdueImpact_FK ON ACTRIS.TX_CorrectiveAction (  CorrectiveActionOverdueImpactID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 
 CREATE NONCLUSTERED INDEX IDX_TX_CorrectiveAction_Priority_FK ON ACTRIS.TX_CorrectiveAction (  CorrectiveActionPriorityID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


	 

	 -- DB_PHE_HSSE_DEV.ACTRIS.TX_ActionTracking foreign keys

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_ActionTracking ADD CONSTRAINT FK_TX_ActionTracking_Source FOREIGN KEY (ActionTrackingSourceID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_ActionTrackingSource(ActionTrackingSourceID);



-- DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionHistory foreign keys

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionHistory ADD CONSTRAINT FK_TX_CorrectiveActionHistory_CorrectiveAction FOREIGN KEY (CorrectiveActionID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction(CorrectiveActionID);


-- DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction foreign keys

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction ADD CONSTRAINT FK_TX_CorrectiveAction_ActionTracking FOREIGN KEY (ActionTrackingID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.TX_ActionTracking(ActionTrackingID);

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction ADD CONSTRAINT FK_TX_CorrectiveAction_OverdueImpact FOREIGN KEY (CorrectiveActionOverdueImpactID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionOverdueImpact(CorrectiveActionOverdueImpactID);

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction ADD CONSTRAINT FK_TX_CorrectiveAction_Priority FOREIGN KEY (CorrectiveActionPriorityID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.MD_CorrectiveActionPriority(CorrectiveActionPriorityID);






CREATE TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionUser (
	CorrectiveActionUserID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CorrectiveActionID varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpAccount varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Role] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpEmail varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	EmpId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyCode varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CompanyName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DirectorateDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DivisionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DepartmentDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PosId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PosTitle varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentPosId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ParentPosTitle varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SectionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SectionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionId varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SubDivisionDesc varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserType varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	CreatedAt datetime DEFAULT getdate() NULL,
	ModifiedBy varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'system' NULL,
	ModifiedAt datetime DEFAULT getdate() NULL,
	DataStatus varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'active' NULL,
	CONSTRAINT PK_TX_CorrectiveActionUser PRIMARY KEY (CorrectiveActionUserID)	
);


CREATE NONCLUSTERED INDEX IDX_TX_CorrectiveActionUser_CorrectiveAction_FK ON ACTRIS.TX_CorrectiveActionUser (  CorrectiveActionID ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 
-- DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionUser foreign keys

ALTER TABLE DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveActionUser ADD CONSTRAINT FK_TX_CorrectiveActionUser_CorrectiveAction FOREIGN KEY (CorrectiveActionID) REFERENCES DB_PHE_HSSE_DEV.ACTRIS.TX_CorrectiveAction(CorrectiveActionID);







