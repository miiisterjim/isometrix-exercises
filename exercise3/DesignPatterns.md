# Design Patterns

## Top 3 Design Patterns
 - **Specification Pattern**
 - Decorator Pattern
 - Builder Pattern

## Favourite Design Pattern - Specification Pattern
The core problems that the Specification pattern can help address are when you require to select a subset of objects based on some critria.  Specifications are decoupled from the object that utilises them.  They are a set of re-usable rules that for a given domain prescribe business logic that can be chained with other specifications as neccessary to build more complex rules.

I have found it particularly useful when queries towards a repository are wide ranging.  Rather than polluting the repository API with many very specific functions to meet the needs of the various services/objects, you can simply expose a single function that accepts a specification object that prescibes the selection criteria.


### UML Diagram

[Diagram](https://en.wikipedia.org/wiki/Specification_pattern#/media/File:Specification_UML.png) (Wikipedia)


## Code Excerpt from a Repoitory i've used this on

An example of where I've used specification pattern can be seen below.  To summarise the three code snippets:

1. The set of specifications that can be used when composing criteria
2. The repository
3. Example service method calling the repository using these specifications 

```
public class GovernanceProposalSpecs {
    public static Specification<GovernanceProposal> statusIn(List<GovernanceProposalStatus> statuses) {
        return (proposal, ec, cb) -> {

            if(statuses == null || statuses.size() == 0) {
                return cb.and();
            }

            return proposal.get("status").in(statuses);
        };
    }

    public static Specification<GovernanceProposal> actionTypeIn(List<GovernanceActionType> actionTypes) {
        return (proposal, ec, cb) -> {

            if(actionTypes == null || actionTypes.size() == 0) {
                return cb.and();
            }

            return proposal.get("actionType").in(actionTypes);
        };
    }

    public static Specification<GovernanceProposal> orderByCreated() {
        return (proposal, ec, cb) ->  {
            ec.orderBy(cb.asc(proposal.get("created")));
            return cb.and();
        };
    }
}
```

```
public interface GovernanceProposalRepository extends PagingAndSortingRepository<GovernanceProposal, UUID>, JpaSpecificationExecutor<GovernanceProposal> {

    List<GovernanceProposal> findBySubjectIdAndActionTypeAndStatusIn(String subjectId, GovernanceActionType actionType, Collection<GovernanceProposalStatus> statuses);
    Optional<GovernanceProposal> findByDid(String did);
    boolean existsByDid(String did);
    void deleteByDid(String did);
}
```

```
Specification spec = Specification
                .where(GovernanceProposalSpecs.actionTypeIn(actionTypes))
                .and(GovernanceProposalSpecs.statusIn(statuses))
                .and(GovernanceProposalSpecs.orderByCreated());

        Page<GovernanceProposal> pagedEntities = governanceProposalRepository.findAll(spec, pageable);
```