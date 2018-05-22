<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:dc="SFA/ILR/2017-18"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" exclude-result-prefixes="dc">
  <xsl:output method="xml" encoding="UTF-8" indent="yes" />
  <xsl:strip-space elements="*"/>
  <xsl:template match="/dc:Message">
    <Message xmlns="SFA/ILR/2017-18">
      <Header >
        <CollectionDetails>
          <Collection>
            <xsl:value-of select="dc:Header/dc:CollectionDetails/dc:Collection"/>
          </Collection>
          <Year>
            <xsl:value-of select="dc:Header/dc:CollectionDetails/dc:Year"/>
          </Year>
          <FilePreparationDate>
            <xsl:value-of select="dc:Header/dc:CollectionDetails/dc:FilePreparationDate"/>
          </FilePreparationDate>
        </CollectionDetails>
        <Source>
          <ProtectiveMarking>
            <xsl:value-of select="dc:Header/dc:Source/dc:ProtectiveMarking"/>
          </ProtectiveMarking>
          <UKPRN>
            <xsl:value-of select="dc:Header/dc:Source/dc:UKPRN"/>
          </UKPRN>
          <SoftwareSupplier>
            <xsl:value-of select="dc:Header/dc:Source/dc:SoftwareSupplier"/>
          </SoftwareSupplier>
          <SoftwarePackage>
            <xsl:value-of select="dc:Header/dc:Source/dc:SoftwarePackage"/>
          </SoftwarePackage>
          <Release>
            <xsl:value-of select="dc:Header/dc:Source/dc:Release"/>
          </Release>
          <SerialNo>
            <xsl:value-of select="dc:Header/dc:Source/dc:SerialNo"/>
          </SerialNo>
          <DateTime>
            <xsl:value-of select="dc:Header/dc:Source/dc:DateTime"/>
          </DateTime>
        </Source>
      </Header>
      <LearningProvider>
        <UKPRN>
          <xsl:value-of select="dc:LearningProvider/dc:UKPRN"/>
        </UKPRN>
      </LearningProvider>
      <xsl:for-each select="dc:Learner">
        <Learner>
          <xsl:if test="dc:LearnRefNumber != ''">
            <LearnRefNumber>
              <xsl:value-of select="dc:LearnRefNumber"/>
            </LearnRefNumber>
          </xsl:if>
          <xsl:if test="dc:PrevLearnRefNumber != ''">
            <PrevLearnRefNumber>
              <xsl:value-of select="dc:PrevLearnRefNumber"/>
            </PrevLearnRefNumber>
          </xsl:if>
          <xsl:if test="dc:PrevUKPRN != ''">
            <PrevUKPRN>
              <xsl:value-of select="dc:PrevUKPRN"/>
            </PrevUKPRN>
          </xsl:if>
          <xsl:if test="dc:PMUKPRN != ''">
            <PMUKPRN>
              <xsl:value-of select="dc:PMUKPRN"/>
            </PMUKPRN>
          </xsl:if>
          <ULN>
            <xsl:value-of select="dc:ULN"/>
          </ULN>
          <xsl:if test="dc:FamilyName != ''">
            <FamilyName>
              <xsl:value-of select="dc:FamilyName"/>
            </FamilyName>
          </xsl:if>
          <xsl:if test="dc:GivenNames != ''">
            <GivenNames>
              <xsl:value-of select="dc:GivenNames"/>
            </GivenNames>
          </xsl:if>
          <xsl:if test="dc:DateOfBirth != ''">
            <DateOfBirth>
              <xsl:value-of select="dc:DateOfBirth"/>
            </DateOfBirth>
          </xsl:if>
          <Ethnicity>
            <xsl:value-of select="dc:Ethnicity"/>
          </Ethnicity>
          <Sex>
            <xsl:value-of select="dc:Sex"/>
          </Sex>
          <LLDDHealthProb>
            <xsl:value-of select="dc:LLDDHealthProb"/>
          </LLDDHealthProb>
          <xsl:if test="dc:NINumber != ''">
            <NINumber>
              <xsl:value-of select="dc:NINumber"/>
            </NINumber>
          </xsl:if>
          <xsl:if test="dc:PriorAttain != ''">
            <PriorAttain>
              <xsl:value-of select="dc:PriorAttain"/>
            </PriorAttain>
          </xsl:if>
          <xsl:if test="dc:Accom != ''">
            <Accom>
              <xsl:value-of select="dc:Accom"/>
            </Accom>
          </xsl:if>
          <xsl:if test="dc:ALSCost != ''">
            <ALSCost>
              <xsl:value-of select="dc:ALSCost"/>
            </ALSCost>
          </xsl:if>
          <xsl:if test="dc:PlanLearnHours != ''">
            <PlanLearnHours>
              <xsl:value-of select="dc:PlanLearnHours"/>
            </PlanLearnHours>
          </xsl:if>
          <xsl:if test="dc:PlanEEPHours != ''">
            <PlanEEPHours>
              <xsl:value-of select="dc:PlanEEPHours"/>
            </PlanEEPHours>
          </xsl:if>
          <xsl:if test="dc:MathGrade != ''">
            <MathGrade>
              <xsl:value-of select="dc:MathGrade"/>
            </MathGrade>
          </xsl:if>
          <xsl:if test="dc:EngGrade != ''">
            <EngGrade>
              <xsl:value-of select="dc:EngGrade"/>
            </EngGrade>
          </xsl:if>
          <PostcodePrior>
            <xsl:value-of select="dc:PostcodePrior"/>
          </PostcodePrior>
          <Postcode>
            <xsl:value-of select="dc:Postcode"/>
          </Postcode>
          <xsl:if test="dc:AddLine1 != ''">
            <AddLine1>
              <xsl:value-of select="dc:AddLine1"/>
            </AddLine1>
          </xsl:if>
          <xsl:if test="dc:AddLine2 != ''">
            <AddLine2>
              <xsl:value-of select="dc:AddLine2"/>
            </AddLine2>
          </xsl:if>
          <xsl:if test="dc:AddLine3 != ''">
            <AddLine3>
              <xsl:value-of select="dc:AddLine3"/>
            </AddLine3>
          </xsl:if>
          <xsl:if test="dc:AddLine4 != ''">
            <AddLine4>
              <xsl:value-of select="dc:AddLine4"/>
            </AddLine4>
          </xsl:if>
          <xsl:if test="dc:TelNo != ''">
            <TelNo>
              <xsl:value-of select="dc:TelNo"/>
            </TelNo>
          </xsl:if>
          <xsl:if test="dc:Email != ''">
            <Email>
              <xsl:value-of select="dc:Email"/>
            </Email>
          </xsl:if>
          <xsl:for-each select="dc:ContactPreference">
            <ContactPreference>
              <ContPrefType>
                <xsl:value-of select="dc:ContPrefType"/>
              </ContPrefType>
              <ContPrefCode>
                <xsl:value-of select="dc:ContPrefCode"/>
              </ContPrefCode>
            </ContactPreference>
          </xsl:for-each>
          <xsl:for-each select="dc:LLDDandHealthProblem">
            <LLDDandHealthProblem>
              <LLDDCat>
                <xsl:value-of select="dc:LLDDCat"/>
              </LLDDCat>
              <xsl:if test="dc:PrimaryLLDD != ''">
                <PrimaryLLDD>
                  <xsl:value-of select="dc:PrimaryLLDD"/>
                </PrimaryLLDD>
              </xsl:if>
            </LLDDandHealthProblem>
          </xsl:for-each>
          <xsl:for-each select="dc:LearnerFAM">
            <LearnerFAM>
              <LearnFAMType>
                <xsl:value-of select="dc:LearnFAMType"/>
              </LearnFAMType>
              <LearnFAMCode>
                <xsl:value-of select="dc:LearnFAMCode"/>
              </LearnFAMCode>
            </LearnerFAM>
          </xsl:for-each>
          <xsl:for-each select="dc:ProviderSpecLearnerMonitoring">
            <ProviderSpecLearnerMonitoring>
              <ProvSpecLearnMonOccur>
                <xsl:value-of select="dc:ProvSpecLearnMonOccur"/>
              </ProvSpecLearnMonOccur>
              <ProvSpecLearnMon>
                <xsl:value-of select="dc:ProvSpecLearnMon"/>
              </ProvSpecLearnMon>
            </ProviderSpecLearnerMonitoring>
          </xsl:for-each>
          <xsl:for-each select="dc:LearnerEmploymentStatus">
            <LearnerEmploymentStatus>
              <EmpStat>
                <xsl:value-of select="dc:EmpStat"/>
              </EmpStat>
              <xsl:if test="dc:DateEmpStatApp != ''">
                <DateEmpStatApp>
                  <xsl:value-of select="dc:DateEmpStatApp"/>
                </DateEmpStatApp>
              </xsl:if>
              <xsl:if test="dc:EmpId != ''">
                <EmpId>
                  <xsl:value-of select="dc:EmpId"/>
                </EmpId>
              </xsl:if>
              <xsl:for-each select="dc:EmploymentStatusMonitoring">
                <EmploymentStatusMonitoring>
                  <ESMType>
                    <xsl:value-of select="dc:ESMType"/>
                  </ESMType>
                  <ESMCode>
                    <xsl:value-of select="dc:ESMCode"/>
                  </ESMCode>
                </EmploymentStatusMonitoring>
              </xsl:for-each>
            </LearnerEmploymentStatus>
          </xsl:for-each>
          <xsl:for-each select="dc:LearnerHE">
            <LearnerHE>
              <xsl:if test="dc:UCASPERID != ''">
                <UCASPERID>
                  <xsl:value-of select="dc:UCASPERID"/>
                </UCASPERID>
              </xsl:if>
              <xsl:if test="dc:TTACCOM != ''">
                <TTACCOM>
                  <xsl:value-of select="dc:TTACCOM"/>
                </TTACCOM>
              </xsl:if>
              <xsl:for-each select="dc:LearnerHEFinancialSupport">
                <LearnerHEFinancialSupport>
                  <FINTYPE>
                    <xsl:value-of select="dc:FINTYPE"/>
                  </FINTYPE>
                  <FINAMOUNT>
                    <xsl:value-of select="dc:FINAMOUNT"/>
                  </FINAMOUNT>
                </LearnerHEFinancialSupport>
              </xsl:for-each>
            </LearnerHE>
          </xsl:for-each>
          <xsl:for-each select="dc:LearningDelivery">
            <LearningDelivery>
              <LearnAimRef>
                <xsl:value-of select="dc:LearnAimRef"/>
              </LearnAimRef>
              <AimType>
                <xsl:value-of select="dc:AimType"/>
              </AimType>
              <AimSeqNumber>
                <xsl:value-of select="dc:AimSeqNumber"/>
              </AimSeqNumber>
              <LearnStartDate>
                <xsl:value-of select="dc:LearnStartDate"/>
              </LearnStartDate>
              <xsl:if test="dc:OrigLearnStartDate != ''">
                <OrigLearnStartDate>
                  <xsl:value-of select="dc:OrigLearnStartDate"/>
                </OrigLearnStartDate>
              </xsl:if>
              <LearnPlanEndDate>
                <xsl:value-of select="dc:LearnPlanEndDate"/>
              </LearnPlanEndDate>
              <FundModel>
                <xsl:value-of select="dc:FundModel"/>
              </FundModel>
              <xsl:if test="dc:ProgType != ''">
                <ProgType>
                  <xsl:value-of select="dc:ProgType"/>
                </ProgType>
              </xsl:if>
              <xsl:if test="dc:FworkCode != ''">
                <FworkCode>
                  <xsl:value-of select="dc:FworkCode"/>
                </FworkCode>
              </xsl:if>
              <xsl:if test="dc:PwayCode != ''">
                <PwayCode>
                  <xsl:value-of select="dc:PwayCode"/>
                </PwayCode>
              </xsl:if>
              <xsl:if test="dc:StdCode != ''">
                <StdCode>
                  <xsl:value-of select="dc:StdCode"/>
                </StdCode>
              </xsl:if>
              <xsl:if test="dc:PartnerUKPRN != ''">
                <PartnerUKPRN>
                  <xsl:value-of select="dc:PartnerUKPRN"/>
                </PartnerUKPRN>
              </xsl:if>
              <DelLocPostCode>
                <xsl:value-of select="dc:DelLocPostCode"/>
              </DelLocPostCode>
              <xsl:if test="dc:AddHours != ''">
                <AddHours>
                  <xsl:value-of select="dc:AddHours"/>
                </AddHours>
              </xsl:if>
              <xsl:if test="dc:PriorLearnFundAdj != ''">
                <PriorLearnFundAdj>
                  <xsl:value-of select="dc:PriorLearnFundAdj"/>
                </PriorLearnFundAdj>
              </xsl:if>
              <xsl:if test="dc:OtherFundAdj != ''">
                <OtherFundAdj>
                  <xsl:value-of select="dc:OtherFundAdj"/>
                </OtherFundAdj>
              </xsl:if>
              <xsl:if test="dc:ConRefNumber != ''">
                <ConRefNumber>
                  <xsl:value-of select="dc:ConRefNumber"/>
                </ConRefNumber>
              </xsl:if>
              <xsl:if test="dc:EPAOrgID != ''">
                <EPAOrgID>
                  <xsl:value-of select="dc:EPAOrgID"/>
                </EPAOrgID>
              </xsl:if>
              <xsl:if test="dc:EmpOutcome != ''">
                <EmpOutcome>
                  <xsl:value-of select="dc:EmpOutcome"/>
                </EmpOutcome>
              </xsl:if>
              <CompStatus>
                <xsl:value-of select="dc:CompStatus"/>
              </CompStatus>
              <xsl:if test="dc:LearnActEndDate != ''">
                <LearnActEndDate>
                  <xsl:value-of select="dc:LearnActEndDate"/>
                </LearnActEndDate>
              </xsl:if>
              <xsl:if test="dc:WithdrawReason != ''">
                <WithdrawReason>
                  <xsl:value-of select="dc:WithdrawReason"/>
                </WithdrawReason>
              </xsl:if>
              <xsl:if test="dc:Outcome != ''">
                <Outcome>
                  <xsl:value-of select="dc:Outcome"/>
                </Outcome>
              </xsl:if>
              <xsl:if test="dc:AchDate != ''">
                <AchDate>
                  <xsl:value-of select="dc:AchDate"/>
                </AchDate>
              </xsl:if>
              <xsl:if test="dc:OutGrade != ''">
                <OutGrade>
                  <xsl:value-of select="dc:OutGrade"/>
                </OutGrade>
              </xsl:if>
              <xsl:if test="dc:SWSupAimId != ''">
                <SWSupAimId>
                  <xsl:value-of select="dc:SWSupAimId"/>
                </SWSupAimId>
              </xsl:if>
              <xsl:for-each select="dc:LearningDeliveryFAM">
                <LearningDeliveryFAM>
                  <LearnDelFAMType>
                    <xsl:value-of select="dc:LearnDelFAMType"/>
                  </LearnDelFAMType>
                  <LearnDelFAMCode>
                    <xsl:value-of select="dc:LearnDelFAMCode"/>
                  </LearnDelFAMCode>
                  <xsl:if test="dc:LearnDelFAMDateFrom != ''">
                    <LearnDelFAMDateFrom>
                      <xsl:value-of select="dc:LearnDelFAMDateFrom"/>
                    </LearnDelFAMDateFrom>
                  </xsl:if>
                  <xsl:if test="dc:LearnDelFAMDateTo != ''">
                    <LearnDelFAMDateTo>
                      <xsl:value-of select="dc:LearnDelFAMDateTo"/>
                    </LearnDelFAMDateTo>
                  </xsl:if>
                </LearningDeliveryFAM>
              </xsl:for-each>
              <xsl:for-each select="dc:LearningDeliveryWorkPlacement">
                <LearningDeliveryWorkPlacement>
                  <WorkPlaceStartDate>
                    <xsl:value-of select="dc:WorkPlaceStartDate"/>
                  </WorkPlaceStartDate>
                  <xsl:if test="dc:WorkPlaceEndDate != ''">
                    <WorkPlaceEndDate>
                      <xsl:value-of select="dc:WorkPlaceEndDate"/>
                    </WorkPlaceEndDate>
                  </xsl:if>
                  <WorkPlaceHours>
                    <xsl:value-of select="dc:WorkPlaceHours"/>
                  </WorkPlaceHours>
                  <WorkPlaceMode>
                    <xsl:value-of select="dc:WorkPlaceMode"/>
                  </WorkPlaceMode>
                  <xsl:if test="dc:WorkPlaceEmpId != ''">
                    <WorkPlaceEmpId>
                      <xsl:value-of select="dc:WorkPlaceEmpId"/>
                    </WorkPlaceEmpId>
                  </xsl:if>
                </LearningDeliveryWorkPlacement>
              </xsl:for-each>
              <xsl:for-each select="dc:AppFinRecord">
                <AppFinRecord>
                  <xsl:if test="dc:AFinType != ''">
                    <AFinType>
                      <xsl:value-of select="dc:AFinType"/>
                    </AFinType>
                  </xsl:if>
                  <xsl:if test="dc:AFinCode != ''">
                    <AFinCode>
                      <xsl:value-of select="dc:AFinCode"/>
                    </AFinCode>
                  </xsl:if>
                  <xsl:if test="dc:AFinDate != ''">
                    <AFinDate>
                      <xsl:value-of select="dc:AFinDate"/>
                    </AFinDate>
                  </xsl:if>
                  <xsl:if test="dc:AFinAmount != ''">
                    <AFinAmount>
                      <xsl:value-of select="dc:AFinAmount"/>
                    </AFinAmount>
                  </xsl:if>
                </AppFinRecord>
              </xsl:for-each>
              <xsl:for-each select="dc:ProviderSpecDeliveryMonitoring">
                <ProviderSpecDeliveryMonitoring>
                  <ProvSpecDelMonOccur>
                    <xsl:value-of select="dc:ProvSpecDelMonOccur"/>
                  </ProvSpecDelMonOccur>
                  <ProvSpecDelMon>
                    <xsl:value-of select="dc:ProvSpecDelMon"/>
                  </ProvSpecDelMon>
                </ProviderSpecDeliveryMonitoring>
              </xsl:for-each>
              <xsl:for-each select="dc:LearningDeliveryHE">
                <LearningDeliveryHE>
                  <xsl:if test="dc:NUMHUS != ''">
                    <NUMHUS>
                      <xsl:value-of select="dc:NUMHUS"/>
                    </NUMHUS>
                  </xsl:if>
                  <xsl:if test="dc:SSN != ''">
                    <SSN>
                      <xsl:value-of select="dc:SSN"/>
                    </SSN>
                  </xsl:if>
                  <xsl:if test="dc:QUALENT3 != ''">
                    <QUALENT3>
                      <xsl:value-of select="dc:QUALENT3"/>
                    </QUALENT3>
                  </xsl:if>
                  <xsl:if test="dc:SOC2000 != ''">
                    <SOC2000>
                      <xsl:value-of select="dc:SOC2000"/>
                    </SOC2000>
                  </xsl:if>
                  <xsl:if test="dc:SEC != ''">
                    <SEC>
                      <xsl:value-of select="dc:SEC"/>
                    </SEC>
                  </xsl:if>
                  <xsl:if test="dc:UCASAPPID  != ''">
                    <UCASAPPID >
                      <xsl:value-of select="dc:UCASAPPID "/>
                    </UCASAPPID >
                  </xsl:if>
                  <TYPEYR>
                    <xsl:value-of select="dc:TYPEYR"/>
                  </TYPEYR>
                  <MODESTUD>
                    <xsl:value-of select="dc:MODESTUD"/>
                  </MODESTUD>
                  <FUNDLEV>
                    <xsl:value-of select="dc:FUNDLEV"/>
                  </FUNDLEV>
                  <FUNDCOMP>
                    <xsl:value-of select="dc:FUNDCOMP"/>
                  </FUNDCOMP>
                  <xsl:if test="dc:STULOAD  != ''">
                    <STULOAD >
                      <xsl:value-of select="dc:STULOAD "/>
                    </STULOAD >
                  </xsl:if>
                  <YEARSTU>
                    <xsl:value-of select="dc:YEARSTU"/>
                  </YEARSTU>
                  <MSTUFEE>
                    <xsl:value-of select="dc:MSTUFEE"/>
                  </MSTUFEE>
                  <xsl:if test="dc:PCOLAB != ''">
                    <PCOLAB>
                      <xsl:value-of select="dc:PCOLAB"/>
                    </PCOLAB>
                  </xsl:if>
                  <xsl:if test="dc:PCFLDCS != ''">
                    <PCFLDCS>
                      <xsl:value-of select="dc:PCFLDCS"/>
                    </PCFLDCS>
                  </xsl:if>
                  <xsl:if test="dc:PCSLDCS != ''">
                    <PCSLDCS>
                      <xsl:value-of select="dc:PCSLDCS"/>
                    </PCSLDCS>
                  </xsl:if>
                  <xsl:if test="dc:PCTLDCS  != ''">
                    <PCTLDCS >
                      <xsl:value-of select="dc:PCTLDCS "/>
                    </PCTLDCS >
                  </xsl:if>
                  <SPECFEE>
                    <xsl:value-of select="dc:SPECFEE"/>
                  </SPECFEE>
                  <xsl:if test="dc:NETFEE != ''">
                    <NETFEE>
                      <xsl:value-of select="dc:NETFEE"/>
                    </NETFEE>
                  </xsl:if>
                  <xsl:if test="dc:GROSSFEE != ''">
                    <GROSSFEE>
                      <xsl:value-of select="dc:GROSSFEE"/>
                    </GROSSFEE>
                  </xsl:if>
                  <xsl:if test="dc:DOMICILE != ''">
                    <DOMICILE>
                      <xsl:value-of select="dc:DOMICILE"/>
                    </DOMICILE>
                  </xsl:if>
                  <xsl:if test="dc:ELQ != ''">
                    <ELQ>
                      <xsl:value-of select="dc:ELQ"/>
                    </ELQ>
                  </xsl:if>
                  <xsl:if test="dc:HEPostCode  != ''">
                    <HEPostCode >
                      <xsl:value-of select="dc:HEPostCode "/>
                    </HEPostCode >
                  </xsl:if>
                </LearningDeliveryHE>
              </xsl:for-each>
            </LearningDelivery>
          </xsl:for-each>
        </Learner>
      </xsl:for-each>
      <xsl:for-each select="dc:LearnerDestinationandProgression">
        <LearnerDestinationandProgression>
          <xsl:if test="dc:LearnRefNumber != ''">
            <LearnRefNumber>
              <xsl:value-of select="dc:LearnRefNumber"/>
            </LearnRefNumber>
          </xsl:if>
          <xsl:if test="dc:ULN != ''">
            <ULN>
              <xsl:value-of select="dc:ULN"/>
            </ULN>
          </xsl:if>
          <xsl:for-each select="dc:DPOutcome">
            <DPOutcome>
              <xsl:if test="dc:OutType  != ''">
                <OutType>
                  <xsl:value-of select="dc:OutType"/>
                </OutType>
              </xsl:if>
              <xsl:if test="dc:OutCode != ''">
                <OutCode>
                  <xsl:value-of select="dc:OutCode"/>
                </OutCode>
              </xsl:if>
              <xsl:if test="dc:OutStartDate  != ''">
                <OutStartDate>
                  <xsl:value-of select="dc:OutStartDate"/>
                </OutStartDate>
              </xsl:if>
              <xsl:if test="dc:OutEndDate  != ''">
                <OutEndDate >
                  <xsl:value-of select="dc:OutEndDate "/>
                </OutEndDate >
              </xsl:if>
              <xsl:if test="dc:OutCollDate  != ''">
                <OutCollDate>
                  <xsl:value-of select="dc:OutCollDate"/>
                </OutCollDate>
              </xsl:if>
            </DPOutcome>
          </xsl:for-each>
        </LearnerDestinationandProgression>
      </xsl:for-each>
    </Message>
  </xsl:template>


</xsl:stylesheet>