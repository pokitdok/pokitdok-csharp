using System.Net.PeerToPeer.Collaboration;

namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Indicates the type of payment for the claim. It is an optional field and when left blank or not passed in the request, defaults to “mutually_defined”
    /// </summary>
    public enum ClaimFilingCode
    {
        /// <summary>
        /// The mutualy defined
        /// </summary>
        MutuallyDefined,

        /// <summary>
        /// The automobile medical
        /// </summary>
        AutomobileMedical,

        /// <summary>
        /// The medicaid
        /// </summary>
        Medicaid,

        /// <summary>
        /// The blue cross blue shield
        /// </summary>
        BlueCrossBlueShield,

        /// <summary>
        /// The medicare part a
        /// </summary>
        MedicarePartA,

        /// <summary>
        /// The champus
        /// </summary>
        Champus,

        /// <summary>
        /// The medicare part b
        /// </summary>
        MedicarePartB,

        /// <summary>
        /// The commercial insurance co
        /// </summary>
        CommercialInsuranceCo,

        /// <summary>
        /// The dental maintenance organization
        /// </summary>
        DentalMaintenanceOrganization,

        /// <summary>
        /// The other non federal program
        /// </summary>
        OtherNonFederalProgram,

        /// <summary>
        /// The epo
        /// </summary>
        Epo,

        /// <summary>
        /// The position
        /// </summary>
        Pos,

        /// <summary>
        /// The federal employee program
        /// </summary>
        FederalEmployeeProgram,

        /// <summary>
        /// The ppo
        /// </summary>
        Ppo,

        /// <summary>
        /// The hmo
        /// </summary>
        Hmo,

        /// <summary>
        /// The title v
        /// </summary>
        TitleV,

        /// <summary>
        /// The hmo medicare risk
        /// </summary>
        HmoMedicareRisk,

        /// <summary>
        /// The veterans affairs plan
        /// </summary>
        VeteransAffairsPlan,

        /// <summary>
        /// The indemnity insurance
        /// </summary>
        IndemnityInsurance,

        /// <summary>
        /// The workers compensation health claim
        /// </summary>
        WorkersCompensationHealthClaim,

        /// <summary>
        /// The liability medicine
        /// </summary>
        LiabilityMedicine
    }

    /// <summary>
    /// Required: The patient’s relationship to the subscriber
    /// </summary>
    public enum Relationship
    {
        /// <summary>
        /// The cadaver_donor
        /// </summary>
        CadaverDonor,

        /// <summary>
        /// The organ_donor
        /// </summary>
        OrganDonor,

        /// <summary>
        /// The child
        /// </summary>
        Child,

        /// <summary>
        /// The other_relationship
        /// </summary>
        OtherRelationship,

        /// <summary>
        /// The employee
        /// </summary>
        Employee,

        /// <summary>
        /// The spouse
        /// </summary>
        Spouse,

        /// <summary>
        /// The life_partner
        /// </summary>
        LifePartner,

        /// <summary>
        /// The unknown
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Enum Gender
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// male
        /// </summary>
        Male,

        /// <summary>
        /// female
        /// </summary>
        Female
    }

    /// <summary>
    /// Required: The type of claim transaction that is being submitted (e.g. “chargeable”). 
    /// </summary>
    public enum TransactionCode
    {
        /// <summary>
        /// The subrogation demand
        /// </summary>
        SubrogationDemand,

        /// <summary>
        /// The chargeable
        /// </summary>
        Chargeable,

        /// <summary>
        /// The reporting
        /// </summary>
        Reporting
    }

    /// <summary>
    /// (Institutional claim specific) The type of claim-related occurrence for specifc dates.
    /// </summary>
    public enum OccurenceType
    {
        /// <summary>
        /// The accident employment related
        /// </summary>
        AccidentEmploymentRelated,

        /// <summary>
        /// The guarantee of payment
        /// </summary>
        GuaranteeOfPayment,

        /// <summary>
        /// The accident medical coverage
        /// </summary>
        AccidentMedicalCoverage,

        /// <summary>
        /// The home iv therapy started
        /// </summary>
        HomeIvTherapyStarted,

        /// <summary>
        /// The accident no medical coverage
        /// </summary>
        AccidentNoMedicalCoverage,

        /// <summary>
        /// The hospice certification
        /// </summary>
        HospiceCertification,

        /// <summary>
        /// The accident tort liability
        /// </summary>
        AccidentTortLiability,

        /// <summary>
        /// The inpatient hospital discharge non covered transplant patient
        /// </summary>
        InpatientHospitalDischargeNonCoveredTransplantPatient,

        /// <summary>
        /// The active care ended
        /// </summary>
        ActiveCareEnded,

        /// <summary>
        /// The inpatient hospital discharge transplant patient
        /// </summary>
        InpatientHospitalDischargeTransplantPatient,

        /// <summary>
        /// The admission scheduled
        /// </summary>
        AdmissionScheduled,

        /// <summary>
        /// The insurance denied
        /// </summary>
        InsuranceDenied,

        /// <summary>
        /// The beneficiary notified of intent to bill accomodations
        /// </summary>
        BeneficiaryNotifiedOfIntentToBillAccomodations,

        /// <summary>
        /// The last menstrual period
        /// </summary>
        LastMenstrualPeriod,

        /// <summary>
        /// The beneficiary notified of intent to bill procedures
        /// </summary>
        BeneficiaryNotifiedOfIntentToBillProcedures,

        /// <summary>
        /// The last therapy
        /// </summary>
        LastTherapy,

        /// <summary>
        /// The benefits exhausted payer a
        /// </summary>
        BenefitsExhaustedPayerA,

        /// <summary>
        /// The no fault insurance involved
        /// </summary>
        NoFaultInsuranceInvolved,

        /// <summary>
        /// The benefits exhausted payer b
        /// </summary>
        BenefitsExhaustedPayerB,

        /// <summary>
        /// The occupational therapy started
        /// </summary>
        OccupationalTherapyStarted,

        /// <summary>
        /// The benefits exhausted payer c
        /// </summary>
        BenefitsExhaustedPayerC,

        /// <summary>
        /// The onset for chronically dependent individual
        /// </summary>
        OnsetForChronicallyDependentIndividual,

        /// <summary>
        /// The benefits terminated primary payer
        /// </summary>
        BenefitsTerminatedPrimaryPayer,

        /// <summary>
        /// The onset of symptoms
        /// </summary>
        OnsetOfSymptoms,

        /// <summary>
        /// The birth date insured a
        /// </summary>
        BirthDateInsuredA,

        /// <summary>
        /// The outpatient occupational therapy plan reviewed
        /// </summary>
        OutpatientOccupationalTherapyPlanReviewed,

        /// <summary>
        /// The birth date insured b
        /// </summary>
        BirthDateInsuredB,

        /// <summary>
        /// The outpatient physical therapy plan reviewed
        /// </summary>
        OutpatientPhysicalTherapyPlanReviewed,

        /// <summary>
        /// The birth date insured c
        /// </summary>
        BirthDateInsuredC,

        /// <summary>
        /// The outpatient speech pathology plan reviewed
        /// </summary>
        OutpatientSpeechPathologyPlanReviewed,

        /// <summary>
        /// The canceled surgery scheduled
        /// </summary>
        CanceledSurgeryScheduled,

        /// <summary>
        /// The physical therapy started
        /// </summary>
        PhysicalTherapyStarted,

        /// <summary>
        /// The cardiac rehab started
        /// </summary>
        CardiacRehabStarted,

        /// <summary>
        /// The pre admission testing
        /// </summary>
        PreAdmissionTesting,

        /// <summary>
        /// The comprehensive outpatient rehab plan reviewed
        /// </summary>
        ComprehensiveOutpatientRehabPlanReviewed,

        /// <summary>
        /// The retirement spouse
        /// </summary>
        RetirementSpouse,

        /// <summary>
        /// The cost outlier status begins
        /// </summary>
        CostOutlierStatusBegins,

        /// <summary>
        /// The retirement
        /// </summary>
        Retirement,

        /// <summary>
        /// The crime victim
        /// </summary>
        CrimeVictim,

        /// <summary>
        /// The SNF bed became available
        /// </summary>
        SnfBedBecameAvailable,

        /// <summary>
        /// The discharge
        /// </summary>
        Discharge,

        /// <summary>
        /// The speech therapy started
        /// </summary>
        SpeechTherapyStarted,

        /// <summary>
        /// The discharged on continuous course iv therapy
        /// </summary>
        DischargedOnContinuousCourseIvTherapy,

        /// <summary>
        /// The split bill date
        /// </summary>
        SplitBillDate,

        /// <summary>
        /// The effective date insured a
        /// </summary>
        EffectiveDateInsuredA,

        /// <summary>
        /// The start coordination period for esrd beneficiaries
        /// </summary>
        StartCoordinationPeriodForEsrdBeneficiaries,

        /// <summary>
        /// The effective date insured b
        /// </summary>
        EffectiveDateInsuredB,

        /// <summary>
        /// The start infertility treatement cycle
        /// </summary>
        StartInfertilityTreatementCycle,

        /// <summary>
        /// The effective date insured c
        /// </summary>
        EffectiveDateInsuredC,

        /// <summary>
        /// The ur notice received
        /// </summary>
        UrNoticeReceived,

        /// <summary>
        /// The election of extended care facilities
        /// </summary>
        ElectionOfExtendedCareFacilities
    }

    /// <summary>
    /// Full list of possible values that can be used in the claim.value_information parameter on the claim
    /// </summary>
    public enum ValueInformationEnum
    {
        /// <summary>
        /// The accident hour
        /// </summary>
        AccidentHour,

        /// <summary>
        /// The medicare blood deductible
        /// </summary>
        MedicareBloodDeductible,

        /// <summary>
        /// Any liability insurance
        /// </summary>
        AnyLiabilityInsurance,

        /// <summary>
        /// The medicare coinsurance amount first year
        /// </summary>
        MedicareCoinsuranceAmountFirstYear,

        /// <summary>
        /// The arterial blood gas
        /// </summary>
        ArterialBloodGas,

        /// <summary>
        /// The medicare coinsurance amount second year
        /// </summary>
        MedicareCoinsuranceAmountSecondYear,

        /// <summary>
        /// The black lung
        /// </summary>
        BlackLung,

        /// <summary>
        /// The medicare lifetime reserve amount first year
        /// </summary>
        MedicareLifetimeReserveAmountFirstYear,

        /// <summary>
        /// The blood deductible pints
        /// </summary>
        BloodDeductiblePints,

        /// <summary>
        /// The medicare lifetime reserve amount second year
        /// </summary>
        MedicareLifetimeReserveAmountSecondYear,

        /// <summary>
        /// The blood pints furnished
        /// </summary>
        BloodPintsFurnished,

        /// <summary>
        /// The medicare new technology add on payment
        /// </summary>
        MedicareNewTechnologyAddOnPayment,

        /// <summary>
        /// The blood pints replaced
        /// </summary>
        BloodPintsReplaced,

        /// <summary>
        /// The medicare spend down amount
        /// </summary>
        MedicareSpendDownAmount,

        /// <summary>
        /// The cardiac rehab visits
        /// </summary>
        CardiacRehabVisits,

        /// <summary>
        /// The most common semi private rate
        /// </summary>
        MostCommonSemiPrivateRate,

        /// <summary>
        /// The catastrophic
        /// </summary>
        Catastrophic,

        /// <summary>
        /// The multiple patient ambulance transport
        /// </summary>
        MultiplePatientAmbulanceTransport,

        /// <summary>
        /// The chiropractic services offset patient payment amount
        /// </summary>
        ChiropracticServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The new coverage not implemented by managed care plan
        /// </summary>
        NewCoverageNotImplementedByManagedCarePlan,

        /// <summary>
        /// The coinsurance days
        /// </summary>
        CoinsuranceDays,

        /// <summary>
        /// The newborn birth weight
        /// </summary>
        NewbornBirthWeight,

        /// <summary>
        /// The coinsurance payer a
        /// </summary>
        CoinsurancePayerA,

        /// <summary>
        /// The no fault insurance
        /// </summary>
        NoFaultInsurance,

        /// <summary>
        /// The coinsurance payer b
        /// </summary>
        CoinsurancePayerB,

        /// <summary>
        /// The non covered days
        /// </summary>
        NonCoveredDays,

        /// <summary>
        /// The coinsurance payer c
        /// </summary>
        CoinsurancePayerC,

        /// <summary>
        /// The occupational therapy visits
        /// </summary>
        OccupationalTherapyVisits,

        /// <summary>
        /// The conventional provider payment amount non demonstration claims
        /// </summary>
        ConventionalProviderPaymentAmountNonDemonstrationClaims,

        /// <summary>
        /// The operating disproportionate share amount
        /// </summary>
        OperatingDisproportionateShareAmount,

        /// <summary>
        /// The copayment payer a
        /// </summary>
        CopaymentPayerA,

        /// <summary>
        /// The operating i ndirect medical education amount
        /// </summary>
        OperatingINdirectMedicalEducationAmount,

        /// <summary>
        /// The copayment payer b
        /// </summary>
        CopaymentPayerB,

        /// <summary>
        /// The operating outlier amount
        /// </summary>
        OperatingOutlierAmount,

        /// <summary>
        /// The copayment payer c
        /// </summary>
        CopaymentPayerC,

        /// <summary>
        /// The other assessments payer a
        /// </summary>
        OtherAssessmentsPayerA,

        /// <summary>
        /// The covered days
        /// </summary>
        CoveredDays,

        /// <summary>
        /// The other assessments payer b
        /// </summary>
        OtherAssessmentsPayerB,

        /// <summary>
        /// The covered self administratable drugs diagnostic study
        /// </summary>
        CoveredSelfAdministratableDrugsDiagnosticStudy,

        /// <summary>
        /// The other assessments payer c
        /// </summary>
        OtherAssessmentsPayerC,

        /// <summary>
        /// The covered self administratable drugs emergency
        /// </summary>
        CoveredSelfAdministratableDrugsEmergency,

        /// <summary>
        /// The other medical services offset patient payment amount
        /// </summary>
        OtherMedicalServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The covered self administrable drugs not self administrable
        /// </summary>
        CoveredSelfAdministrableDrugsNotSelfAdministrable,

        /// <summary>
        /// The oxygen saturation
        /// </summary>
        OxygenSaturation,

        /// <summary>
        /// The deductible payer a
        /// </summary>
        DeductiblePayerA,

        /// <summary>
        /// The part_ a_ demonstration payment
        /// </summary>
        Part_A_DemonstrationPayment,

        /// <summary>
        /// The deductible payer b
        /// </summary>
        DeductiblePayerB,

        /// <summary>
        /// The part_ b_ coinsurance
        /// </summary>
        Part_B_Coinsurance,

        /// <summary>
        /// The deductible payer c
        /// </summary>
        DeductiblePayerC,

        /// <summary>
        /// The part_ b_ demonstration payment
        /// </summary>
        Part_B_DemonstrationPayment,

        /// <summary>
        /// The dental services offset patient payment amount
        /// </summary>
        DentalServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The patient estimated responsibility
        /// </summary>
        PatientEstimatedResponsibility,

        /// <summary>
        /// The disabled beneficiary under_65_ LGHP
        /// </summary>
        DisabledBeneficiaryUnder_65_Lghp,

        /// <summary>
        /// The patient height
        /// </summary>
        PatientHeight,

        /// <summary>
        /// The eligibility threshold charity care
        /// </summary>
        EligibilityThresholdCharityCare,

        /// <summary>
        /// The patient liability amount
        /// </summary>
        PatientLiabilityAmount,

        /// <summary>
        /// The epo units provided
        /// </summary>
        EpoUnitsProvided,

        /// <summary>
        /// The patient weight
        /// </summary>
        PatientWeight,

        /// <summary>
        /// The esrd beneficiary in medicare coordination period with eghp
        /// </summary>
        EsrdBeneficiaryInMedicareCoordinationPeriodWithEghp,

        /// <summary>
        /// The peritoneal dialysis
        /// </summary>
        PeritonealDialysis,

        /// <summary>
        /// The esrd network funding
        /// </summary>
        EsrdNetworkFunding,

        /// <summary>
        /// The PHS
        /// </summary>
        PHS,

        /// <summary>
        /// The estimated responsibility payer a
        /// </summary>
        EstimatedResponsibilityPayerA,

        /// <summary>
        /// The physical therapy visits
        /// </summary>
        PhysicalTherapyVisits,

        /// <summary>
        /// The estimated responsibility payer b
        /// </summary>
        EstimatedResponsibilityPayerB,

        /// <summary>
        /// The podiactric services offset patient payment amount
        /// </summary>
        PodiactricServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The estimated responsibility payer c
        /// </summary>
        EstimatedResponsibilityPayerC,

        /// <summary>
        /// The prescription drugs offset patient payment amount
        /// </summary>
        PrescriptionDrugsOffsetPatientPaymentAmount,

        /// <summary>
        /// The flat rate surgery charge
        /// </summary>
        FlatRateSurgeryCharge,

        /// <summary>
        /// The professional charges included and billed seperately
        /// </summary>
        ProfessionalChargesIncludedAndBilledSeperately,

        /// <summary>
        /// The grace days
        /// </summary>
        GraceDays,

        /// <summary>
        /// The provider amount agreed to accept primary payer
        /// </summary>
        ProviderAmountAgreedToAcceptPrimaryPayer,

        /// <summary>
        /// The health insurance premiums offset patient payment amount
        /// </summary>
        HealthInsurancePremiumsOffsetPatientPaymentAmount,

        /// <summary>
        /// The providers interim rate
        /// </summary>
        ProvidersInterimRate,

        /// <summary>
        /// The hearing ear services offset patient payment amount
        /// </summary>
        HearingEarServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The recurring monthly income
        /// </summary>
        RecurringMonthlyIncome,

        /// <summary>
        /// The hematocrit reading
        /// </summary>
        HematocritReading,

        /// <summary>
        /// The regulatory surcharges payer a
        /// </summary>
        RegulatorySurchargesPayerA,

        /// <summary>
        /// The hemoglobin reading
        /// </summary>
        HemoglobinReading,

        /// <summary>
        /// The regulatory surcharges payer b
        /// </summary>
        RegulatorySurchargesPayerB,

        /// <summary>
        /// The hh reimbursements part a
        /// </summary>
        HhReimbursementsPartA,

        /// <summary>
        /// The regulatory surcharges payer c
        /// </summary>
        RegulatorySurchargesPayerC,

        /// <summary>
        /// The hh reimbursements part b
        /// </summary>
        HhReimbursementsPartB,

        /// <summary>
        /// The service furnished location number
        /// </summary>
        ServiceFurnishedLocationNumber,

        /// <summary>
        /// The hh visits part a
        /// </summary>
        HhVisitsPartA,

        /// <summary>
        /// The skilled nurse home visit hours
        /// </summary>
        SkilledNurseHomeVisitHours,

        /// <summary>
        /// The hh visits part b
        /// </summary>
        HhVisitsPartB,

        /// <summary>
        /// The special zip code reporting
        /// </summary>
        SpecialZipCodeReporting,

        /// <summary>
        /// The hha branch msa
        /// </summary>
        HhaBranchMsa,

        /// <summary>
        /// The speech therapy visits
        /// </summary>
        SpeechTherapyVisits,

        /// <summary>
        /// The home health aide home visit hours
        /// </summary>
        HomeHealthAideHomeVisitHours,

        /// <summary>
        /// The state charity care percent
        /// </summary>
        StateCharityCarePercent,

        /// <summary>
        /// The hospital no semi private rooms
        /// </summary>
        HospitalNoSemiPrivateRooms,

        /// <summary>
        /// The surplus
        /// </summary>
        Surplus,

        /// <summary>
        /// The inpatient professional charges combined billed
        /// </summary>
        InpatientProfessionalChargesCombinedBilled,

        /// <summary>
        /// The veterans affairs
        /// </summary>
        VeteransAffairs,

        /// <summary>
        /// The interest amount
        /// </summary>
        InterestAmount,

        /// <summary>
        /// The vision eye services offset patient payment amount
        /// </summary>
        VisionEyeServicesOffsetPatientPaymentAmount,

        /// <summary>
        /// The lifetime reserve days
        /// </summary>
        LifetimeReserveDays,

        /// <summary>
        /// The workers compensation
        /// </summary>
        WorkersCompensation,

        /// <summary>
        /// The medicaid rate code
        /// </summary>
        MedicaidRateCode,

        /// <summary>
        /// The working age beneficiary spouse with eghp
        /// </summary>
        WorkingAgeBeneficiarySpouseWithEghp
    }

    /// <summary>
    /// Full list of possible values that can be used in the claim.patient_status parameter on the claim
    /// </summary>
    public enum PatientStatus
    {
        /// <summary>
        /// The expired at home
        /// </summary>
        ExpiredAtHome,

        /// <summary>
        /// The transferred to hospice medical facility
        /// </summary>
        TransferredToHospiceMedicalFacility,

        /// <summary>
        /// The expired in medical facility
        /// </summary>
        ExpiredInMedicalFacility,

        /// <summary>
        /// The transfered to hospice medical facility
        /// </summary>
        TransferedToHospiceMedicalFacility,

        /// <summary>
        /// The expired place unknown
        /// </summary>
        ExpiredPlaceUnknown,

        /// <summary>
        /// The transferred to inpatient rehab
        /// </summary>
        TransferredToInpatientRehab,

        /// <summary>
        /// The expired
        /// </summary>
        Expired,

        /// <summary>
        /// The transferred to intermediate care facility
        /// </summary>
        TransferredToIntermediateCareFacility,

        /// <summary>
        /// The inpatient at this hospital
        /// </summary>
        InpatientAtThisHospital,

        /// <summary>
        /// The transferred to long term care hospital
        /// </summary>
        TransferredToLongTermCareHospital,

        /// <summary>
        /// The left against medical advice
        /// </summary>
        LeftAgainstMedicalAdvice,

        /// <summary>
        /// The transferred to nursing facility not medicare certified
        /// </summary>
        TransferredToNursingFacilityNotMedicareCertified,

        /// <summary>
        /// The routine discharge
        /// </summary>
        RoutineDischarge,

        /// <summary>
        /// The transferred to other health care institution
        /// </summary>
        TransferredToOtherHealthCareInstitution,

        /// <summary>
        /// The still patient
        /// </summary>
        StillPatient,

        /// <summary>
        /// The transferred to psychiatric hospital
        /// </summary>
        TransferredToPsychiatricHospital,

        /// <summary>
        /// The transferred to cancer center
        /// </summary>
        TransferredToCancerCenter,

        /// <summary>
        /// The transferred to short term hospital
        /// </summary>
        TransferredToShortTermHospital,

        /// <summary>
        /// The transferred to critical access hospital
        /// </summary>
        TransferredToCriticalAccessHospital,

        /// <summary>
        /// The transferred to skilled nursing facility
        /// </summary>
        TransferredToSkilledNursingFacility,

        /// <summary>
        /// The transferred to federal hospital
        /// </summary>
        TransferredToFederalHospital,

        /// <summary>
        /// The transferred to swing bed
        /// </summary>
        TransferredToSwingBed,

        /// <summary>
        /// The transferred to home with home health service
        /// </summary>
        TransferredToHomeWithHomeHealthService
    }

    /// <summary>
    /// Full list of possible values that can be used in the claim.place_of_service parameter on the claim
    /// </summary>
    public enum PlaceOfService
    {
        /// <summary>
        /// The ambulance air or water
        /// </summary>
        AmbulanceAirOrWater,

        /// <summary>
        /// The mobile unit
        /// </summary>
        MobileUnit,

        /// <summary>
        /// The ambulance land
        /// </summary>
        AmbulanceLand,

        /// <summary>
        /// The nursing
        /// </summary>
        Nursing,

        /// <summary>
        /// The assisted living
        /// </summary>
        AssistedLiving,

        /// <summary>
        /// The office
        /// </summary>
        Office,

        /// <summary>
        /// The birthing center
        /// </summary>
        BirthingCenter,

        /// <summary>
        /// The other
        /// </summary>
        Other,

        /// <summary>
        /// The custodial
        /// </summary>
        Custodial,

        /// <summary>
        /// The outpatient hospital
        /// </summary>
        OutpatientHospital,

        /// <summary>
        /// The end stage renal
        /// </summary>
        EndStageRenal,

        /// <summary>
        /// The outpatient rehab
        /// </summary>
        OutpatientRehab,

        /// <summary>
        /// The er hospital
        /// </summary>
        ErHospital,

        /// <summary>
        /// The pharmacy
        /// </summary>
        Pharmacy,

        /// <summary>
        /// The federal qualified
        /// </summary>
        FederalQualified,

        /// <summary>
        /// The prison
        /// </summary>
        Prison,

        /// <summary>
        /// The group home
        /// </summary>
        GroupHome,

        /// <summary>
        /// The psych partial hospital
        /// </summary>
        PsychPartialHospital,

        /// <summary>
        /// The home
        /// </summary>
        Home,

        /// <summary>
        /// The public clinic
        /// </summary>
        PublicClinic,

        /// <summary>
        /// The hospice
        /// </summary>
        Hospice,

        /// <summary>
        /// The residential substance abuse
        /// </summary>
        ResidentialSubstanceAbuse,

        /// <summary>
        /// The ihs freestanding
        /// </summary>
        IhsFreestanding,

        /// <summary>
        /// The rural clinic
        /// </summary>
        RuralClinic,

        /// <summary>
        /// The ihs provider
        /// </summary>
        IhsProvider,

        /// <summary>
        /// The school
        /// </summary>
        School,

        /// <summary>
        /// The immunization
        /// </summary>
        Immunization,

        /// <summary>
        /// The shelter
        /// </summary>
        Shelter,

        /// <summary>
        /// The independent clinic
        /// </summary>
        IndependentClinic,

        /// <summary>
        /// The skilled nursing
        /// </summary>
        SkilledNursing,

        /// <summary>
        /// The independent lab
        /// </summary>
        IndependentLab,

        /// <summary>
        /// The surgical center
        /// </summary>
        SurgicalCenter,

        /// <summary>
        /// The inpatient hospital
        /// </summary>
        InpatientHospital,

        /// <summary>
        /// The temporary lodging
        /// </summary>
        TempLodging,

        /// <summary>
        /// The inpatient psych
        /// </summary>
        InpatientPsych,

        /// <summary>
        /// The tribal_638 freestanding
        /// </summary>
        Tribal_638Freestanding,

        /// <summary>
        /// The inpatient rehab
        /// </summary>
        InpatientRehab,

        /// <summary>
        /// The tribal_638 provider
        /// </summary>
        Tribal_638Provider,

        /// <summary>
        /// The mental health center
        /// </summary>
        MentalHealthCenter,

        /// <summary>
        /// The urgent care
        /// </summary>
        UrgentCare,

        /// <summary>
        /// The mentally retarded
        /// </summary>
        MentallyRetarded,

        /// <summary>
        /// The walkin clinic
        /// </summary>
        WalkinClinic,

        /// <summary>
        /// The military
        /// </summary>
        Military,

        /// <summary>
        /// The worksite
        /// </summary>
        Worksite
    }

    /// <summary>
    /// Full list of possible values that can be used in the claim.facility_type parameter on the claim
    /// </summary>
    public enum FacilityType
    {
        /// <summary>
        /// The clinic corf
        /// </summary>
        ClinicCorf,

        /// <summary>
        /// The hospital inpatient part b
        /// </summary>
        HospitalInpatientPartB,

        /// <summary>
        /// The clinic ersd
        /// </summary>
        ClinicErsd,

        /// <summary>
        /// The hospital other part b
        /// </summary>
        HospitalOtherPartB,

        /// <summary>
        /// The clinic opt
        /// </summary>
        ClinicOpt,

        /// <summary>
        /// The hospital outpatient asc
        /// </summary>
        HospitalOutpatientAsc,

        /// <summary>
        /// The clinic rural health
        /// </summary>
        ClinicRuralHealth,

        /// <summary>
        /// The hospital outpatient
        /// </summary>
        HospitalOutpatient,

        /// <summary>
        /// The community mental health center
        /// </summary>
        CommunityMentalHealthCenter,

        /// <summary>
        /// The hospital swing bed
        /// </summary>
        HospitalSwingBed,

        /// <summary>
        /// The critical access hospital
        /// </summary>
        CriticalAccessHospital,

        /// <summary>
        /// The nonhospital based hospice
        /// </summary>
        NonhospitalBasedHospice,

        /// <summary>
        /// The federally qualified health center
        /// </summary>
        FederallyQualifiedHealthCenter,

        /// <summary>
        /// The religious nonmedical institution
        /// </summary>
        ReligiousNonmedicalInstitution,

        /// <summary>
        /// The home health part b
        /// </summary>
        HomeHealthPartB,

        /// <summary>
        /// The skilled nursing inpatient part b
        /// </summary>
        SkilledNursingInpatientPartB,

        /// <summary>
        /// The home health
        /// </summary>
        HomeHealth,

        /// <summary>
        /// The skilled nursing inpatient
        /// </summary>
        SkilledNursingInpatient,

        /// <summary>
        /// The hospital based hospice
        /// </summary>
        HospitalBasedHospice,

        /// <summary>
        /// The skilled nursing outpatient
        /// </summary>
        SkilledNursingOutpatient,

        /// <summary>
        /// The hospital inpatient part a
        /// </summary>
        HospitalInpatientPartA,

        /// <summary>
        /// The skilled nursing swing bed
        /// </summary>
        SkilledNursingSwingBed
    }

    /// <summary>
    /// Full list of possible values that can be used in the claim.admission_type parameter on the claim
    /// </summary>
    public enum AdmissionType
    {
        /// <summary>
        /// The elective
        /// </summary>
        Elective,

        /// <summary>
        /// The newborn
        /// </summary>
        Newborn,

        /// <summary>
        /// The emergency
        /// </summary>
        Emergency,

        /// <summary>
        /// The trauma center
        /// </summary>
        TraumaCenter,

        /// <summary>
        /// The information not available
        /// </summary>
        InformationNotAvailable,

        /// <summary>
        /// The urgent care
        /// </summary>
        Urgent
    }

    /// <summary>
    /// (Institutional claim specific) The source of the patient’s admission.
    /// </summary>
    public enum AdmissionSource
    {
        /// <summary>
        /// The clinic
        /// </summary>
        Clinic,

        /// <summary>
        /// The immediate care facility
        /// </summary>
        ImmediateCareFacility,

        /// <summary>
        /// The emergency room
        /// </summary>
        EmergencyRoom,

        /// <summary>
        /// The law enforcement
        /// </summary>
        LawEnforcement,

        /// <summary>
        /// The health care facility
        /// </summary>
        HealthCareFacility,

        /// <summary>
        /// The not available
        /// </summary>
        NotAvailable,

        /// <summary>
        /// The hospice transfer
        /// </summary>
        HospiceTransfer,

        /// <summary>
        /// The physician referral
        /// </summary>
        PhysicianReferral,

        /// <summary>
        /// The hospital transfer
        /// </summary>
        HospitalTransfer,

        /// <summary>
        /// The surgery center
        /// </summary>
        SurgeryCenter
    }
}